from pathlib import Path
from Crypto.Cipher import AES
from Crypto.Util.Padding import unpad
import gzip
import hashlib

source = Path("payload.exe")
data = source.read_bytes()

key = bytes.fromhex(
    "8f45160b01eeb87d5b680054776ae7cf"
    "2ed6ea2b62c50d47b0b2c3e54f46a72a"
)

iv = bytes.fromhex("d21aebef8b59d8fe478c307f46e668da")

# AES-256-CBC decryption
cipher = AES.new(key, AES.MODE_CBC, iv)
decrypted = unpad(cipher.decrypt(data), AES.block_size)

# GZIP decompression
payload = gzip.decompress(decrypted)

output = Path("payload_decoded.bin")
output.write_bytes(payload)

print("Decrypted size:", len(decrypted))
print("Final payload size:", len(payload))
print("SHA256:", hashlib.sha256(payload).hexdigest())
print("First 16 bytes:", payload[:16].hex(" "))
print("MZ header:", payload.startswith(b"MZ"))
print("Saved:", output)