const webpack = require('webpack');

function getClientEnvironment(configuration) {
  // Grab NODE_ENV and GATE_* environment variables and prepare them to be
  // injected into the application via DefinePlugin in webpack configuration.
  const gateEnvExpr = /^GATE_/i;
  const raw = Object.keys(process.env)
    .filter((key) => gateEnvExpr.test(key))
    .reduce(
      (env, key) => {
        env[key] = process.env[key];
        return env;
      },
      {
        NODE_ENV: process.env.NODE_ENV || configuration,
      }
    );

  // Stringify all values so we can feed into webpack DefinePlugin
  return {
    'process.env': Object.keys(raw).reduce((env, key) => {
      env[key] = JSON.stringify(raw[key]);
      return env;
    }, {}),
  };
}

module.exports = (config, options, context) => {
  config.plugins.push(
    new webpack.DefinePlugin(getClientEnvironment(context.configuration))
  );
  return config;
};
