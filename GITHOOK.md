
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

npm run lint
npm run format
npm test
```

> Commit will fails at this stage unless you add at least one application or library. If you wish to commit before creating an application or library, do not update yet the husky.sh script.

## **Commit message linting**

The second part of the pre-commit validation process is to validate the commit message. This is to ensure consistency in the format so that changelog can be generated automatically during CI build.

```bash
npm install @commitlint/config-conventional @commitlint/cli -D

npx husky add .husky/commit-msg 'npx --no-install commitlint --edit'
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
        "ci",
        "chore",
        "docs",
        "feat",
        "fix",
        "perf",
        "refactor",
        "revert",
        "style"
      ]
    ]
  }
}
```

# Powered by

``` 
Powered by
  _   _ _            _                  ______               _____                      
 | \ | (_)          | |                |  ____|             / ____|                     
 |  \| |_ _ __   ___| |_ ___  ___ _ __ | |__ ___  _   _ _ _| (___   _____   _____ _ __  
 | . ` | | '_ \ / _ \ __/ _ \/ _ \ '_ \|  __/ _ \| | | | '__\___ \ / _ \ \ / / _ \ '_ \ 
 | |\  | | | | |  __/ ||  __/  __/ | | | | | (_) | |_| | |  ____) |  __/\ V /  __/ | | |
 |_| \_|_|_| |_|\___|\__\___|\___|_| |_|_|  \___/ \__,_|_| |_____/ \___| \_/ \___|_| |_|
```
