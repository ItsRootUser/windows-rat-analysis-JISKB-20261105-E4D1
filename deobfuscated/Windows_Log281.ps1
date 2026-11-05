$ErrorActionPreference = 'Stop'

$FilePath = "$env:APPDATA\Windows_Log_281.bat"
$OutputDir = Join-Path $PWD 'extracted'

New-Item -ItemType Directory -Path $OutputDir -Force |
    Out-Null

# Original constants from the sample
$Key = [Convert]::FromBase64String(
    'OBZY87IWjxYjhH+v0cDMUN/22Y7qoXn7ry5gAIyhPd4='
)

$IV = [Convert]::FromBase64String(
    'eHeIQOTht7iUW2y1fFTtQQ=='
)

function ConvertFrom-EncryptedPayload {
    param([string]$Encoded)

    $Encoded = $Encoded.Replace('#', '/').Replace('@', 'A')
    $Ciphertext = [Convert]::FromBase64String($Encoded)

    $AES = [System.Security.Cryptography.Aes]::Create()
    $AES.Mode = 'CBC'
    $AES.Padding = 'PKCS7'
    $AES.Key = $Key
    $AES.IV = $IV

    try {
        $Decryptor = $AES.CreateDecryptor()

        try {
            $Decryptor.TransformFinalBlock(
                $Ciphertext, 0, $Ciphertext.Length
            )
        }
        finally {
            $Decryptor.Dispose()
        }
    }
    finally {
        $AES.Dispose()
    }
}

function Expand-GzipPayload {
    param([byte[]]$Data)

    $InputStream = [IO.MemoryStream]::new(,$Data)
    $OutputStream = [IO.MemoryStream]::new()

    try {
        $Gzip = [IO.Compression.GZipStream]::new(
            $InputStream,
            [IO.Compression.CompressionMode]::Decompress
        )

        try {
            $Gzip.CopyTo($OutputStream)
            return ,$OutputStream.ToArray()
        }
        finally {
            $Gzip.Dispose()
        }
    }
    finally {
        $InputStream.Dispose()
        $OutputStream.Dispose()
    }
}

$Lines = [IO.File]::ReadAllLines($FilePath)

$PayloadLine = $Lines |
    Where-Object {
        $_.StartsWith('jSSWbcagrSXuLKXWnTwJ')
    } |
    Select-Object -First 1

if (-not $PayloadLine) {
    throw 'Payload marker not found'
}

$EncodedPayloads = $PayloadLine.Substring(20).Split([char]92)

if ($EncodedPayloads.Count -ne 2) {
    throw "Expected two payloads, found $($EncodedPayloads.Count)"
}

for ($i = 0; $i -lt 2; $i++) {
    Write-Host "Processing stage $($i + 1)..."

    $Decrypted = ConvertFrom-EncryptedPayload $EncodedPayloads[$i]
    $Decompressed = Expand-GzipPayload $Decrypted

    $Destination = Join-Path $OutputDir "stage$($i + 1).bin"

    [IO.File]::WriteAllBytes($Destination, $Decompressed)

    $Signature = [BitConverter]::ToString(
        $Decompressed, 0, [Math]::Min(16, $Decompressed.Length)
    )

    Write-Host "Size: $($Decompressed.Length) bytes"
    Write-Host "Signature: $Signature"
    Write-Host "Saved: $Destination"
}

Write-Host 'Extraction complete. No assemblies were executed.'