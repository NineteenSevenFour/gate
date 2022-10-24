
# **CI / CD**

In this section, we will setup the `CI / CD` using Github Actions.

## **Github action**

First, create the`.github/workflows` folder structure and add an empty `.gitkeep` file in each folder. Inside the lower `workflows` folder, create a `ci.yaml` file and add the folowing code.

```yaml
name: Portal CI

on:
  workflow_dispatch:
  push:
    branches: ['main']
  pull_request:
    branches: ['main']
    types: [opened, reopened, synchronize]

jobs:
  build:
    runs-on: self-hosted

    strategy:
      matrix:
        node-version: [16.x]

    steps:
      - uses: actions/checkout@v3
        with:
          ref: ${{ github.event.pull_request.head.ref }}
          fetch-depth: 0
      - name: Fetch other branches # so that nx affected works.
        run: git fetch --no-tags --prune --depth=5 origin main

      - name: Setup Node.js ${{ matrix.node-version }}
        uses: actions/setup-node@v3
        with:
          node-version: ${{ matrix.node-version }}

      - name: Prepare
        run: |
          npm ci

      - name: Check
        run: |
          nx affected:lint -- --base="origin/main"
          nx affected:format -- --base="origin/main"

      - name: Test
        run: nx affected:test -- --base="origin/main" --codeCoverage

      - name: Build
        run: |
          npm run build
```

## **Github packages registry**

In order to allow other team to build application for the portal, we must publish our `Portal SDK` composed of various npm packages. We will use the Github packages registry, but any other registry will works too and will need the pipeline to be adapted accordingly.

Update the `package.json` and add the code coverage section with the following.

```yaml
  "repository": "https://github.com/datatunning/portal-core.git",
  "publishConfig": {
    "registry":"https://npm.pkg.github.com"
  },
```

## **Codecov.io support**

Optionally, update the `ci.yaml` and add the code coverage section as follow.

First find the step named `test` and ass `--codeCoverage` at the end of the run command.

```yaml
- name: Test
  run: |
    nx affected:test -- --base="origin/main" --codeCoverage
- name: Codecov
  # You may pin to the exact commit or the version.
  # uses: codecov/codecov-action@d9f34f8cd5cb3b3eb79b3e4b5dae3a16df499a70
  uses: codecov/codecov-action@v3.1.1
  with:
    # Repository upload token - get it from codecov.io. Required only for private repositories
    token: ${{ secrets.CODECOV_TOKEN }}
    # Comma-separated list of files to upload
    files: # optional
    # Directory to search for coverage reports.
    directory: # optional
    # Flag upload to group coverage metrics (e.g. unittests | integration | ui,chrome)
    flags: # optional
    # The commit SHA of the parent for which you are uploading coverage. If not present, the parent will be determined using the API of your repository provider.  When using the repository providers API, the parent is determined via finding the closest ancestor to the commit.
    commit_parent: # optional
    # Don't upload files to Codecov
    dry_run: # optional
    # Environment variables to tag the upload with (e.g. PYTHON | OS,PYTHON)
    env_vars: # optional
    # Specify whether or not CI build should fail if Codecov runs into an error during upload
    fail_ci_if_error: # optional
    # Path to coverage file to upload
    file: # optional
    # Comma-separated list, see the README for options and their usage
    functionalities: # optional
    # Run with gcov support
    gcov: # optional
    # Extra arguments to pass to gcov
    gcov_args: # optional
    # Paths to ignore during gcov gathering
    gcov_ignore: # optional
    # Paths to include during gcov gathering
    gcov_include: # optional
    # Move discovered coverage reports to the trash
    move_coverage_to_trash: # optional
    # User defined upload name. Visible in Codecov UI
    name: # optional
    # Specify whether the Codecov output should be verbose
    verbose: # optional
    # Directory in which to execute codecov.sh
    working-directory: # optional
    # Run with xcode support
    xcode: # optional
    # Specify the xcode archive path. Likely specified as the -resultBundlePath and should end in .xcresult
    xcode_archive_path: # optional
```

To get the coverage data in the proper format, edit the `packages.json` and add or update the following section.

```yaml
  "nyc": {
    "report-dir": "coverage",
    "reporter": ["text", "json", "lcov"],
    "all": true,
    "include": [
      "src/**/*.js"
    ],
    "exclude": [
      "**/*.test.js",
      "**/test.js",
      "**/*.stories.js",
      "**/stories.js"
    ]
  },
```

> > CODE COVERAGE ONLY GENERATES HTML, MAY NOT WORK WITH codecov.io . codecov.io SELF-HOSTED EXE/PIPELINE REMAINS TO BUILD/TEST

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
