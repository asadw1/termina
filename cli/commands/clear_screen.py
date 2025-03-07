import os
from ascii.intro import display_intro

def clear_screen():
    # Check the operating system
    if os.name == 'nt':  # For Windows
        _ = os.system('cls')
    else:  # For Mac and Linux (posix)
        _ = os.system('clear')
    display_intro()  # Display the intro message after clearing the screen
