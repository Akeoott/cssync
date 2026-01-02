# Merge dev into main

## Summary
<!-- High-level overview of what this PR contains -->
[brief summary of major changes].

## Changes
<!-- Add Sections like code or docs depending on the changes -->
<!-- Order pull requests from lowest to highest number, aka first #8 then #9 then #10 -->

<!--
Available sections (use what's relevant, keep this order):
### Features
### Fixes
### Refactor
### Docs
### Workflow
### Other
-->

### Features
* `PullRequestUrl` by @`username`
    * `PullRequestTitle`
        * Key detail 1
        * Key detail 2

### Fixes
* `PullRequestUrl` by @`username`
    * `PullRequestTitle`
        * What was fixed
        * Impact of fix


<!-- Example: -->
<!--
# Merge dev into main
## Changes

### Feature
* https://github.com/Akeoott/cssync/pull/12 by @Akeoott
    * Refactor core with logging, config system, and enhanced CLI
        * Remove separate cssync.Cli project and integrate CLI functionality directly into backend
        * Replace System.Text.Json with Newtonsoft.Json for flexible JObject-based configuration
        * Implement dynamic config sections (Variables, Timers) instead of rigid dictionaries
        * Add comprehensive argument parsing (--help, --status, --init) with categorized help system
        * Introduce config-aware execution loop that responds to runtime changes
        * Improve cross-platform terminal detection (Windows/Unix compatibility)
        * Update README, .editorconfig, and .gitignore to reflect new architecture
        * Add detailed configuration documentation via Resources.cs
### Docs and Workflow
* https://github.com/Akeoott/cssync/pull/8 by @Akeoott
    * Improve documentation and add PR template
        * Add some small changes to editorconfig, readme and vscode settings.
        * Mainly for improving workspace configuration and adding more detail to readme.

etc...
-->