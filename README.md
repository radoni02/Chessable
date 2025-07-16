# Chess Library

A comprehensive C# library for implementing chess game logic and functionality. This library provides all the core components needed to build chess applications, including piece movement validation, game state management, and chess rule enforcement.

## Features

### Core Components
- **Complete Chess Board Implementation** - Full 8x8 board with field management
- **All Chess Pieces** - King, Queen, Rook, Bishop, Knight, and Pawn with accurate movement patterns
- **Move Validation** - Comprehensive validation for legal moves according to chess rules
- **Game State Management** - Track game progress, turn management, and game status

### Chess Mechanics
- **Piece Movement** - Accurate implementation of how each piece moves
- **Attack Patterns** - Calculate which squares are under attack
- **Special Moves** - Support for castling, en passant, and pawn promotion
- **Check Detection** - Automatic detection of check and checkmate conditions
- **Move History** - Track and manage game move history

### Architecture
- **Modular Design** - Clean separation of concerns with abstract base classes
- **Extensible** - Easy to extend with custom pieces or game variants
- **Type-Safe** - Strong typing throughout the library
- **Performance Optimized** - Efficient algorithms for move calculation and validation

## Installation

```bash
# Once published to NuGet
dotnet add package Chessable
```

## Quick Start

## Project Structure

## Usage Examples

### Creating a Custom Game

### Working with Pieces

### Board Management


## Development Status

This library is currently under active development. The following features are implemented:

- ✅ Basic board structure
- ✅ All chess pieces with movement logic
- ✅ Move calculation algorithms
- ✅ Attack pattern detection
- ✅ Special moves (castling, en passant)
- ✅ Check/checkmate detection
- ✅ Game state management
- 🚧 Move history and undo functionality
- 📋 Planned: AI integration support
- 📋 Planned: PGN notation support
- 📋 Planned: FEN string import/export

## Contributing

This project is open for contributions! Areas where help is welcomed:

- Bug fixes and testing
- Performance optimizations
- Additional chess variants
- Documentation improvements
- Example applications

## Requirements

- .NET 8.0

*This library aims to provide a solid foundation for any chess-related application in C#. Whether you're building a chess game, analysis tool, or educational software, this library provides the core functionality you need.*
