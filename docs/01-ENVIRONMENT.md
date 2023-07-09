# **Environment setup**

The toolchain is based on entreprise battle tested tools.

- Workspace management: `nx`, `nrwl`, `create-nx-workspace`
- Source management: `git`, `gitflow`, `husky`, `commitlint`
- Dev tools: `angular`, `typescript`, `primeng`, `transloco`, `ngrx`, `rxjs`

## Dev tools

As a developper/contributor, you are responsible to install the proper tools on your machine. those includes

- GIT +  Tools: TortoiseGit, SourceTree (comes with GitFlow)
- GitFlow
- Any code editor of your choice. Visual Studio code is the one used by myself, but you can pickcthe one you prefers.
- Postman: To test API in 'PRODUCTION' configuration as swagger is  only included in the 'DEV' configuration.
- Fiddler: To `spy` on the network, browser devtools are nice but not as powerfull.

## **Node.JS**

On windows, ensure `nvm` is installed, then run `nvm list` to check the installed node version if any. For our needs and at time of writing, we will use version `18.14.1` of node due to the tech stack dependencies.

Run `nvm install <node_version>` to install node.js verison then `nvm use <node_version>` to activate the newly installed version.

## **Global packages**

Some packeages are required to build the application skeleton. First runs `npm list -g --depth=0` to list the globally installed packages, then runs the following commands (adapt based on what you already have or need to update)

```bash
npm install -g @angular/cli @ngrx/schematics @nrwl/cli @nrwl/schematics @nrwl/workspace create-nx-workspace npm nx rimraf
```

> As of writing this, latest angular major version is `15` but some dependencies may not yet be updated to latest angular version.
> TIP: You can audit global packages using the following command.

```bash
npx npm-global-audit
```

## Powered by

```text
  _   _ _            _                   _____                      ______               
 | \ | (_)          | |                 / ____|                    |  ____|              
 |  \| |_ _ __   ___| |_ ___  ___ _ __ | (___   _____   _____ _ __ | |__ ___  _   _ _ __ 
 | . ` | | '_ \ / _ \ __/ _ \/ _ \ '_ \ \___ \ / _ \ \ / / _ \ '_ \|  __/ _ \| | | | '__|
 | |\  | | | | |  __/ ||  __/  __/ | | |____) |  __/\ V /  __/ | | | | | (_) | |_| | |   
 |_| \_|_|_| |_|\___|\__\___|\___|_| |_|_____/ \___| \_/ \___|_| |_|_|  \___/ \__,_|_|
```
