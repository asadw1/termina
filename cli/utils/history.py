# Module for managing command history

command_history = []

def add_to_history(command):
    if len(command_history) >= 100:
        command_history.pop(0)
    command_history.append(command)

def print_history():
    for index, command in enumerate(command_history, 1):
        print(f"{index}: {command}")
