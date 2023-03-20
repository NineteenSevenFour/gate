
# **Setup toolchain**

To ensure a uniform dev experience, we will enforce some rules via `git hooks`.

## **Pre-commit validation**

`husky` provides hook into git workflow. thefollowing commands will install the package and initialize the hook.

```bash
npm install husky -D
npx husky-init
```

To reduce risk of failed CI build, we need to run the linting, formating and testing command before the commit is completed.

Open the file `.husky/pre-commit` and update as follow

```bash
#!/usr/bin/env sh
. "$(dirname -- "$0")/_/husky.sh"

FILES=$(git diff --cached --name-only --diff-filter=ACMR | sed 's| |\\ |g')
[ -z "$FILES" ] && exit 0

nx affected --target=lint
nx affected --target=format
nx affected --target=test

# Add back the modified/prettified files to staging
echo "$FILES" | xargs git add

exit 0
```

> Commit will fails at this stage unless you add at least one application or library. If you wish to commit before creating an application or library, do not update yet the husky.sh script.

## **Commit message linting**

The second part of the pre-commit validation process is to validate the commit message. This is to ensure consistency in the format so that changelog can be generated automatically during CI build.

```bash
npm install -D @commitlint/config-conventional @commitlint/cli
npx husky add .husky/commit-msg 'npx --no-install commitlint --edit $1'
```

Rules declarations for commit messages are stored in file named `.commitlintrc.json` and paste the following code into it:

```json
{
  "extends": ["@commitlint/config-conventional"],
  "rules": {
    "type-enum": [
      2,
      "always",
      [
        "bug",
        "chore",
        "ci",
        "doc",
        "feat",
        "fix",
        "perf",
        "refactor",
        "revert",
        "ui"
      ]
    ]
  }
}
```

```text
Powered by
  _   _ _            _                  ______               _____                      
 | \ | (_)          | |                |  ____|             / ____|                     
 |  \| |_ _ __   ___| |_ ___  ___ _ __ | |__ ___  _   _ _ _| (___   _____   _____ _ __  
 | . ` | | '_ \ / _ \ __/ _ \/ _ \ '_ \|  __/ _ \| | | | '__\___ \ / _ \ \ / / _ \ '_ \ 
 | |\  | | | | |  __/ ||  __/  __/ | | | | | (_) | |_| | |  ____) |  __/\ V /  __/ | | |
 |_| \_|_|_| |_|\___|\__\___|\___|_| |_|_|  \___/ \__,_|_| |_____/ \___| \_/ \___|_| |_|
```
