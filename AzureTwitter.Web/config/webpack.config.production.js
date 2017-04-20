const webpack = require('webpack');
const webpackMerge = require('webpack-merge');

module.exports = webpackMerge(require('./webpack.config.common'), {
    plugins: [
        new webpack.DefinePlugin({
            PRODUCTION: JSON.stringify(true),
			API: JSON.stringify("http://localhost:8344/"),
			HUB: JSON.stringify("http://localhost:8344/")
        })
    ]
});