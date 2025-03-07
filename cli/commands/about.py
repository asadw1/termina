def display_about():
    about_text = """
    Termina: Your Personal Unix-style Shell for Seamless Music Playback

    Version: 1.0.0
    Developed by: Your Name/Your Company

    Termina is a powerful and intuitive command-line interface designed to provide a seamless music playback experience. With a range of commands at your disposal, you can easily control your music, navigate through your playlist, and enjoy your favorite tunes without leaving the terminal.

    Key Features:
    - Play, pause, stop, skip, and go back to previous tracks
    - View and manage your current playlist
    - Access detailed information about the currently playing song
    - Keep track of your command history

    For more information, visit our website: github.com/asadw1

    Thank you for using Termina!
    """

    def wrap_text(text, width=100):
        lines = text.split('\n')
        wrapped_lines = []
        for line in lines:
            while len(line) > width:
                wrapped_lines.append(line[:width])
                line = line[width:]
            wrapped_lines.append(line)
        return '\n'.join(wrapped_lines)

    wrapped_about_text = wrap_text(about_text)
    print(wrapped_about_text)

if __name__ == "__main__":
    display_about()
