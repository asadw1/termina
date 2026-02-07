# Termina

<img width="1337" height="456" alt="image" src="https://github.com/user-attachments/assets/a9909483-6f79-456c-870a-d877faeceb5c" />


---

Welcome to **Termina**! Inspired by a few past projects, Termina is a multi-component project consisting of a RESTful API backend, a React-based web frontend, and Python CLI tools. This project provides a seamless music player experience with a shell-like interface.

## Overview

Termina consists of three main components:
1. **Backend**: A RESTful API built with ASP.NET Core (C#) that handles music playback and serves as the central hub for communication between components.
2. **Frontend**: A web-based UI built using React, providing a shell-like interface for users to input commands, display the current song information, and show an ASCII-like visualization for the track's length and position.
3. **CLI Tools**: Command-line interfaces for both Linux and Windows CMD, developed using Python, that send requests to the backend API and display results in the terminal.

## Repository Structure

```plaintext
termina/
│
├── backend/
│   └── MusicShellApi/
│       └── [backend files and .sln file]
│
├── frontend/
│   └── [React frontend files]
│
└── cli/
    └── [Python CLI tools files]
```
## Quick Start - One-Click Runner

**From repo root** (where `termina_runner.py` lives):

**Windows:**
```cmd
pip install requests psutil
python termina_runner.py
```
**Linux/MacOS**
```cmd
pip install requests psutil
python3 termina_runner.py
```

## Getting Started

To get started with Termina, follow the instructions for each component:

1. **Backend Setup**:
   - Navigate to the `backend` directory and follow the setup instructions in the `README.md` file located there.

2. **Frontend Setup**:
   - Navigate to the `frontend` directory and follow the setup instructions in the `README.md` file located there.

3. **CLI Tools Setup**:
   - Navigate to the `cli` directory and follow the setup instructions in the `README.md` file located there.

## Contributing

We welcome contributions to Termina! If you would like to contribute, please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License.

## Contact

For questions or support, please reach out to the project maintainer.
