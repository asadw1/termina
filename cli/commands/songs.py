from utils.api import get_all_songs, current_song
from utils.constants import ERROR_COMMAND_FAILED

def execute():
    try:
        songs = get_all_songs()
        current = current_song()
        current_index = None
        
        # Find current song index by title match
        if current and songs:
            for i, song in enumerate(songs):
                if song['title'] == current['title']:
                    current_index = i
                    break
        
        if songs:
            print(f"{len(songs)} songs in library:{' LIVE' if current else ''}")
            print("   ID  Title                                    Duration Artist")
            print("   " + "-" * 78)
            for i, song in enumerate(songs):
                marker = "▶ " if i == current_index else "  "
                print(f"   {marker}{i:2d}. {song['title'][:42]:<42} {song['duration']:<6} {song['artist']}")
        else:
            print("No songs found")
    except Exception as e:
        print(f"Error: {e}")
