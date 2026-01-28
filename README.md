# PacketParser

A C# WinForms application for parsing payment transaction packets. This tool accepts hexadecimal packet input and displays parsed results in a structured format.

## Features

- **Complete Packet Parsing**: Handles all 29 fields from STX to ETX
- **TLV Support**: Parses Tag-Length-Value sections in "Other Details"
- **Multiple Data Types**: Binary amounts, dates, ASCII strings, hex values
- **GUI Interface**: Easy-to-use Windows Forms application
- **Console Testing**: Built-in test mode for validation

## Usage

### GUI Mode
1. Run the application
2. Paste your hex packet data into the input field
3. Click "Parse" to see the structured results

### Console Test Mode
```bash
dotnet run --test
```

## Sample Input/Output

**Input:**
```
02 D2 00 A2 01 31 30 30 30 30 30 30 30 30 30 30 30 30 30 31 32 30 30 30 30 30 30 32 00 00 00 05 34 32 11 35 34 35 35 32 32 2A 2A 2A 2A 2A 2A 2A 34 34 34 35 01 2F 00 00 04 56 49 53 41 07 04 38 37 30 30 00 00 15 00 02 32 07 59 37 53 4A 45 31 00 00 36 32 37 33 36 32 38 31 39 32 37 32 01 30 30 00 3D 01 0B 56 49 53 41 20 43 52 45 44 49 54 02 0E 41 30 30 30 30 30 30 30 30 33 31 30 31 30 03 10 39 38 33 34 39 36 42 33 31 43 36 33 42 46 36 42 0D 0C 36 33 37 32 38 31 37 32 36 33 36 32 11 10 25 11 33 09 16 05 03
```

**Output:**
- STX: 02
- Command ID: D2 - Void Command
- Transaction Amount: 534.32
- Card PAN: 545522*******4445
- Card Type: VISA
- Response Code: 00 - Success
- Transaction Date: Nov. 10, 2025 11:33:09
- And 22 more fields...

## Project Structure

```
NTTPacketParser/
├── Helpers/
│   ├── HexReader.cs          # Binary data reader
│   ├── HexUtils.cs           # Hex conversion utilities
│   ├── PosMessageParser.cs   # Main packet parser
│   └── TlvParser.cs          # TLV field parser
├── Models/
│   ├── ParsedField.cs        # Main field model
│   └── TlvField.cs           # TLV field model
├── InputForm.cs              # Main input form
├── ResultForm.cs             # Results display form
└── Program.cs                # Application entry point
```

## Requirements

- .NET 8.0 or later
- Windows (WinForms application)

## Building

```bash
dotnet build
dotnet run
```