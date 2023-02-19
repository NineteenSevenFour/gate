# **Environment setup**

The toolchain is based on entreprise battle tested tools.

- Workspace management: `nx`, `nrwl`, `create-nx-workspace`
- Source management: `git`, `husky`, `commitlint`
- Dev tools: `angular`, `typescript`, `primeng`, `transloco`

## **Node.JS**

On windows, ensure `nvm` is installed, then run `nvm list` to check the installed node version if any. For our needs and at time of writing, we will use version `16.15.1` of node due to the tech stack dependencies.

> On some windows setup, this version is not working. In this case, you can fallback onto version `16.13.2`

Run `nvm install <node_version>` to install node.js verison then `nvm use <node_version>` to activate the newly installed version.

## **Global packages**

Some packeages are required to build the application skeleton. First runs `npm list -g --depth=0` to list the globally installed packages, then runs the following commands (adapt based on what you already have or need to update)

```bash
npm install -g nx@13 @nrwl/cli@13 @nrwl/workspace@13 create-nx-workspace@13 @ngrx/schematics@13 @angular/cli@13 @nrwl/schematics rimraf
```

> Note that we are using version `13` on the whole toolcahin to match angular version.

> As of writing this, latest angular version is `14` but some dependencies may not yet be updated to latest angular version.

> TIP: You can audit global packages using the following command.
> ```bash 
> npx npm-global-audit
> ```

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
