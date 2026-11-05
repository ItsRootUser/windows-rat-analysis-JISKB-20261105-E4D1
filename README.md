# Windows RAT – Static Analysis & Reverse Engineering

**⚠️ ONLY FOR EDUCATIONAL OR RESEARCH PURPOSES**

**Independent Security Research**  
**SK-CERT Report Reference:** `JISKB-20261105-E4D1`  
**Analysis Date:** November 2026  
**Status:** Reported to SK-CERT and domain abuse contacts

## Overview

This repository documents an independent security research investigation into a suspicious multi-stage Windows malware sample identified on a personal computer.

Through static analysis, deobfuscation and reverse engineering of the recovered .NET components, the investigation identified extensive functionality consistent with a Remote Access Trojan (RAT).

The research focused on understanding the malware's execution chain, cryptographic implementation, persistence mechanisms, information collection, credential and cryptocurrency wallet targeting, surveillance capabilities, command-and-control (C2) communication and security evasion techniques.

The findings were submitted to Slovakia's national cybersecurity authority, SK-CERT. The identified C2 domain was also reported to the relevant abuse contacts.

## Investigation

The investigation began after identifying a suspicious batch file:

`Windows_Log281.bat`

The file contained heavily obfuscated code, prompting further static analysis and deobfuscation.

Multiple executable components and .NET assemblies were subsequently extracted and examined.

### Analysis Stages

| Stage | Description |
|---|---|
| Stage 1 | Obfuscated loader analysis, decryption and payload extraction |
| Stage 2 | Loader analysis, AES decryption, GZIP decompression and in-memory assembly loading |
| Payload | Reverse engineering of the recovered .NET RAT functionality |

## Identified Capabilities

Static analysis revealed the following capabilities.

### 1. Information Collection

The malware collects extensive information about the infected system and its user, including:

- Hardware identification and system specifications.
- Operating system and architecture.
- Username and executable location.
- Administrator privileges.
- Active window information.
- Installed antivirus and security software information.
- Camera availability.
- Malware version and configuration details.
- Client activity and online status.
- Potential cryptocurrency wallet-related information.

Collected information is serialized before being prepared for transmission to the remote server.

### 2. Keylogging

The malware implements keyboard monitoring capable of:

- Capturing low-level keyboard events.
- Translating virtual key codes into readable characters.
- Handling Shift and Caps Lock states.
- Recording special keys.
- Associating captured input with active window titles.
- Storing recorded keystrokes locally.

This functionality creates a risk of credential theft and exposure of sensitive information entered by the user.

### 3. Cryptocurrency and Browser Data Targeting

The malware contains functionality targeting cryptocurrency wallets and information stored by web browsers.

This indicates potential targeting of:

- Cryptocurrency wallet information.
- Browser-stored data.
- Potentially sensitive user information.

The precise scope of accessible information and successful collection has not been established.

### 4. Camera Access

The malware attempts to access available camera and video input devices.

This functionality could potentially enable unauthorized surveillance if successfully activated.

Actual camera activation or transmission of camera data has not been confirmed.

### 5. Command-and-Control Communication

The malware establishes communication with a configured remote C2 server using TCP and TLS.

Identified functionality includes:

- TLS-protected communication channels.
- Server certificate validation.
- Initial transmission of collected client information.
- Structured MessagePack packet exchange.
- Periodic keepalive messages.
- Remote delivery of additional .NET modules.
- Decompression and in-memory execution of received modules.

### 6. Persistence

The malware implements mechanisms intended to maintain execution across system restarts and user sessions.

These include modifications to Windows startup mechanisms and scheduled execution.

### 7. Security Evasion and Anti-Analysis

Multiple techniques are used to interfere with security software and hinder investigation, including:

- AMSI manipulation.
- Interference with Microsoft Defender.
- Termination of security and analysis-related processes.
- Anti-analysis checks.
- Malicious Windows registry modifications.
- Additional low-level Windows manipulation.

These mechanisms can make detection, investigation and removal more difficult.

## Command-and-Control Configuration

The embedded configuration was decrypted and its integrity verified using HMAC-SHA256.

The recovered configuration contained:

| Attribute | Value |
|---|---|
| Malware Group | BloxStrap |
| Version | 1.0.7 |
| Configured Protocol | TLS |
| Configured Port | 666/TCP |
| C2 Hostname | Redacted |

The hostname has been intentionally redacted from this public repository.

**Important:** The recovered hostname represents a configured C2 endpoint. Its current operational status and any actual communication with it have not been verified.

## Payload Module Breakdown (Client.Fafnir)

The recovered payload is organized into a small set of .NET helper classes that implement encryption, persistence, communication, evasion and collection features. The following summary describes the role of each file observed in the payload module:

| File | Purpose |
|---|---|
| `Aes256.cs` | Implements a custom AES-256-CBC encrypt/decrypt routine using PBKDF2-derived keys, PKCS7 padding and an HMAC-SHA256 authentication tag to protect configuration and data objects. |
| `Amsi.cs` | Attempts to bypass AMSI by locating the AMSI DLL and patching its scanning routine in memory. |
| `Anti_Analysis.cs` | Performs a lightweight anti-VM check by querying `Win32_CacheMemory` and exits the process if the environment appears non-standard. |
| `AntiProcess.cs` | Enumerates running processes and kills known security, debugging or analysis tools such as Task Manager, Process Hacker, antivirus services and registry utilities. |
| `Camera.cs` | Uses the COM device enumerator to detect installed video capture devices and determine whether a camera is available. |
| `ClientSocket.cs` | Main C2 communication class: establishes TLS-encrypted socket connections, sends initial client telemetry, keeps the connection alive, parses MessagePack command packets, and loads plugin modules from the server. |
| `HwidGen.cs` | Creates a stable host identifier by hashing machine/user/OS/storage characteristics into an MD5-derived HWID string. |
| `IdSender.cs` | Builds the initial `ClientInfo` message including HWID, username, OS version, administrator status, active window, antivirus status, wallet detection results and online/idle status. |
| `Methods.cs` | Central utility class for admin detection, idle checks, active-window retrieval, wallet discovery, antivirus enumeration, and other host data collection routines. |
| `MutexControl.cs` | Creates and closes a named mutex to enforce a single-instance execution model. |
| `NativeMethods.cs` | Declares native Windows APIs used for window state checks and process criticality configuration. |
| `NormalStartup.cs` | Copies the malware to its install directory and creates persistence by writing a Run key for user-level installs or a scheduled task for elevated installs. |
| `ProcessCritical.cs` | Attempts to mark the process as critical and to prevent normal termination by hooking session-ending events and invoking the native `RtlSetProcessIsCritical` API. |
| `PROCESSENTRY32.cs` | Native process snapshot structure used by `AntiProcess` to iterate through active Windows processes. |
| `Ragnar.cs` | Implements a low-level keyboard hook that captures keystrokes with active-window context and writes them to `winLog.txt` in the user profile. |
| `SetRegistry.cs` | Stores and retrieves binary plugin or state blobs under a per-host registry key under `HKCU\Software\<HWID>`. |
| `Win32.cs` | Helper layer for loading and binding Win32 API delegates at runtime, including memory protection and API resolution logic used in AMSI-related patching. |

The overall design indicates a modular RAT payload: initial host enumeration, anti-analysis and kill-switch logic, local persistence, keystroke capture, C2 communication and plugin delivery. The recovered module set is consistent with a remote-access implant designed to remain stealthy while collecting and exfiltrating sensitive user and system data.

## Cryptographic Analysis

The investigation included examination of the malware's cryptographic implementation.

Identified mechanisms included:

- AES-256-CBC encryption.
- PKCS7 padding.
- PBKDF2-based key derivation.
- HMAC-SHA256 integrity verification.
- RSA-SHA256 signature verification of configuration-related data.

The analysis also covered embedded configuration handling and certificate validation mechanisms.

## Methodology

The investigation was conducted using static analysis techniques:

1. Identification of suspicious files.
2. Deobfuscation of the initial loader.
3. Extraction of embedded components.
4. Decryption and decompression of embedded payloads.
5. .NET assembly decompilation and source-code inspection.
6. Analysis of persistence and security evasion mechanisms.
7. Examination of information collection and surveillance functionality.
8. Analysis of cryptographic implementation.
9. Recovery and verification of embedded configuration.
10. Documentation and responsible disclosure.

No connection was established with the identified C2 infrastructure.

## Responsible Disclosure

The findings were submitted to Slovakia's national cybersecurity authority:

**SK-CERT**

Report reference:

`JISKB-20261105-E4D1`

The identified C2 domain was also reported to the domain registrar and relevant abuse contacts for investigation and potential abuse mitigation.

The reports include technical findings regarding the malware's capabilities, recovered C2 configuration and associated infrastructure.

No confirmation of domain suspension or other enforcement action has been received at the time of writing.

## Limitations

This research was conducted through static analysis.

The following have NOT been confirmed:

- Successful execution of the malware.
- Successful infection of a system.
- Active C2 infrastructure.
- Successful communication with the C2 server.
- Actual data exfiltration.
- Successful cryptocurrency wallet compromise.
- Successful browser data theft.
- Actual camera activation or surveillance.
- The identity of the malware operator.
- The actual extent of damage caused by the sample.

All documented capabilities are based on analysis of the recovered code and configuration.

## Safety Notice

This repository is intended exclusively for educational and defensive cybersecurity research.

It does distribute the original malware samples or functional payloads.

Do not execute unknown malware samples on production systems. Any further dynamic analysis should be performed in a properly isolated laboratory environment.

---

**Research Type:** Malware Analysis / Reverse Engineering / Threat Intelligence  
**Platform:** Windows  
**Primary Technology:** .NET / C#  
**Disclosure:** Reported to SK-CERT and domain abuse contacts  
**Reference:** `JISKB-20261105-E4D1`
