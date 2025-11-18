# Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the LGPL-3.0 License.
# See the LICENSE file in the repository root for full license text.

import platform
import sys

from helper.logConfig import logger


class main:
    # Will contain future functionality
    pass


if __name__ == "__main__":
    os: str = platform.system()
    release: str = platform.release()

    if os == "Linux":
        logger.info(f"{os}: {release}")
        logger.warning(f"{os} support is WIP!")

    elif os == "Windows":
        logger.info(f"{os}: {release}")
        logger.warning(f"{os} is not yet supported!")

    elif os == "Darwin":
        logger.info(f"{os}: {release}")
        logger.warning(f"{os} is not yet supported!")

    else:
        logger.error(f"I couldn't recognize your OS. Sorry! {os}: {release}")
