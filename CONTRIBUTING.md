# Commit messages:

This repo uses a mix of **`Conventional Commits`** and **`angular contributing`** to make commit messages more readable and unified.<br>
***"A specification for adding human and machine readable meaning to commit messages"***

## Quick Summary:
### Commit Message Format
```md
<type>(<scope>): <subject>

<body>

<footer>
```

The header is mandatory and the scope of the header is optional.

Any line of the commit message cannot be longer 100 characters! This allows the message to be easier to read on GitHub as well as in various git tools.

The footer should contain a closing reference to an issue if any.

### Revert
If the commit reverts a previous commit, it should begin with `revert: `, followed by the header of the reverted commit. In the body it should say: `This reverts commit <hash>.`, where the hash is the SHA of the commit being reverted.

### Type
- **docs**: Documentation only changes<br>
- **feat**: A new feature<br>
- **fix**: A bug fix<br>
- **perf**: A code change that improves performance<br>
- **refactor**: A code change that neither fixes a bug nor adds a feature<br>
- **style**: Changes that do not affect the meaning of the code<br>
- **test**: Adding missing tests or correcting existing tests<br>

### Subject
The subject contains a succinct description of the change:

use the imperative, present tense: "change" not "changed" nor "changes"
don't capitalize the first letter
no dot (.) at the end

### Body
Just as in the subject, use the imperative, present tense: "change" not "changed" nor "changes". The body should include the motivation for the change and contrast this with previous behavior.

### Footer
The footer should contain any information about **Breaking Changes** and is also the place to reference GitHub issues that this commit **Closes**.

**Breaking Changes** should start with the word `BREAKING CHANGE:` with a space or two newlines. The rest of the commit message is then used for this.

---

Sources:<br>
[Conventional Commits 1.0.0](https://www.conventionalcommits.org/en/v1.0.0/)<br>
[Angular Contributing](https://github.com/angular/angular/blob/22b96b9/CONTRIBUTING.md#-commit-message-guidelines)
