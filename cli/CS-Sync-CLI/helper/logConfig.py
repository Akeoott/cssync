# Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the LGPL-3.0 License.
# See the LICENSE file in the repository root for full license text.

from loguru import logger


def configureLogger() -> None:
    """Contains all logging configurations"""
    # Will contain future configurations
    pass


if __name__ == "__main__":
    configureLogger()

    def func(a, b) -> float:
        return a / b

    def nested(c) -> None:
        try:
            func(5, c)
        except ZeroDivisionError:
            logger.exception("What?!")

    logger.info("That's it, beautiful and simple logging!")
    logger.debug(
        "If you're using Python {}, prefer {feature} of course!",
        3.6,
        feature="f-strings",
    )
    nested(0)
    logger.info("That was a test exception...")
