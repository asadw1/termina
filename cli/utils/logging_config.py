import logging
import os
from datetime import datetime

# Create logs directory if it doesn't exist
if not os.path.exists('logs'):
    os.makedirs('logs')

# Generate log file name with current time
log_filename = datetime.now().strftime("logs/termina_%m%d%Y%H.%M.%S_debug_log.log")

# Configure logging
logging.basicConfig(
    filename=log_filename,
    level=logging.ERROR,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)

# Create a logger
logger = logging.getLogger('TerminaLogger')
