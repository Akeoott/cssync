# Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the LGPL-3.0 License.
# See the LICENSE file in the repository root for full license text.

import platform
import sys


if __name__ == "__main__":
    os: str = platform.system()
    release: str = platform.release()

    if os == "Linux":
        print(f"Linux({os}): {release}")

    elif os == "Windows":
        print(f"Windows({os}): {release}")
        print(f"Windows({os}) is not yet supported!")

    elif os == "Darwin":
        print(f"MacOS({os}): {release}")
        print(f"MacOS({os}) is not yet supported!")

    else:
        print(f"I couldn't recognize your OS. Sorry!\n{os}: {release}")
