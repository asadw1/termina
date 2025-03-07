import requests
from utils.constants import API_URL, ERROR_UNABLE_TO_CONNECT, ERROR_HTTP, ERROR_REQUEST

def send_request(endpoint, method='GET', data=None):
    try:
        url = f"{API_URL}/{endpoint}"
        if method == 'POST':
            response = requests.post(url, json=data)
        else:
            response = requests.get(url)
        response.raise_for_status()
        return response.json()
    except requests.exceptions.ConnectionError:
        print(ERROR_UNABLE_TO_CONNECT)
        return None
    except requests.exceptions.HTTPError as http_err:
        print(ERROR_HTTP.format(http_err))
        return None
    except requests.exceptions.RequestException as req_err:
        print(ERROR_REQUEST.format(req_err))
        return None

def play():
    return send_request("play", method='POST')

def pause():
    return send_request("pause", method='POST')

def stop():
    return send_request("stop", method='POST')

def next_song():
    return send_request("next", method='POST')

def previous_song():
    return send_request("previous", method='POST')

def current_song(index):
    return send_request(f"songs/{index}")
