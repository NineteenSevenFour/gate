
# **Micro frontend with NX/Angular**

This template allows for both `Host` and `Remote` micro-frontend application scaffolding. You can use different workspace for each or use one workspace with one host and many remotes and/or libraries.

## **Initialize the workspace**

We will use the `create-nx-workspace` package to generate the workspace. Refers to the [NX website](https://nx.dev/nx/create-nx-workspace) for additional information.

```bash
npx create-nx-workspace MyApp --name MyApp --style scss --preset apps --nxCloud false --skipGit true --ci github
```

> TIP
> Run the command without arguments to have the interactive mode on.
> Using something like `@MyOrgMyApp` as first parameter will create a nested folder structure, but the application name wil be MyApp.

As an alternative, if creating from github App or Lib template, you can initialize the existing git repo as follow:

```bash
npx nx@latest init
```

The select the following options from the interactive mode:

 -`Integrated monorepo:  Nx configures your favorite frameworks and lets you focus on shipping features.`
 -`apps                  [an empty monorepo with no plugins with a layout that works best for building apps]`
 -`No | Yes` (that's your choice to use the distributed Cloud CI)
 -Then mode all the files from the init subfolder to the root folder...yeahh...it's a shame NX doesn't seems to have a better way.

## **Scaffold a Host application**

To create the `host` application and optional libraries specific to the application, We will use the `@nrwl/angular` nx plugin generator.

First, install the NX angular generator plugin.

```bash
npm install @nrwl/angular@13 -D
```

Once the angular plugin is installed, we can generate the host application for our portal.

```bash
nx g @nrwl/angular:host host --prefix=portal-host --dynamic --backendProject=http://localhost:9999/api --style=scss --strict
```

> INFO: `--dynamic` allows for runtime micro-frontend rather than static.
> TIP: Add `--dry-run` to validate the options will generate what´d you expect.

## **Scaffold a Remote application**

Alternatively, we can generate a workspace for a remote application instead of a Host. You can even have both Host and Remote within the same workspace.

```bash
nx g @nrwl/angular:remote remote --host=host --port 8101 --prefix=portal-remote --backendProject=http://localhost:9999/api --strict --style=scss
```

> TIP: run `nx g rm Myapp` to remove the application or library.

## **Scaffold a library**

Architecturing a micro-frontend is no more different than any other application. As such, we can create libraries that will be componsed of services, model, api (interfaces) and even components.

```bash
nx g @nrwl/angular:lib MyLib --prefix=portal-mylib --importPath=@portal/mylib --buildable --strict
```

### **Create first service**

To add a service within the newly created `MyLib` library. run the following commmand.

```bash
nx g @nrwl/angular:service MyService --project=portal-mylib
```

Once the service has been added, ensure to add it to the `provider` section of your library module as well as to the `index` or `public-api` file to ensure your service is accessible when referenced by other applications or libraries.

### **Create first component**

To add a componet within the newly created `MyLib` library. run the following commmand.

```bash
nx g @nrwl/angular:component MyComponent --project=portal-mylib
```

Once the component has been added, ensure to add it to the `declaration` and `export` sections of your library module as well as to the `index` or `public-api` file to ensure your component is accessible when referenced by other applications or libraries.

## Powered by

```text
  _   _ _            _                   _____                      ______               
 | \ | (_)          | |                 / ____|                    |  ____|              
 |  \| |_ _ __   ___| |_ ___  ___ _ __ | (___   _____   _____ _ __ | |__ ___  _   _ _ __ 
 | . ` | | '_ \ / _ \ __/ _ \/ _ \ '_ \ \___ \ / _ \ \ / / _ \ '_ \|  __/ _ \| | | | '__|
 | |\  | | | | |  __/ ||  __/  __/ | | |____) |  __/\ V /  __/ | | | | | (_) | |_| | |   
 |_| \_|_|_| |_|\___|\__\___|\___|_| |_|_____/ \___| \_/ \___|_| |_|_|  \___/ \__,_|_|
```
