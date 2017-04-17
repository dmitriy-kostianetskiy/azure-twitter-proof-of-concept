const webpack = require('webpack');
const webpackMerge = require('webpack-merge');

module.exports = webpackMerge(require('./webpack.config.common'), {
    plugins: [
        new webpack.DefinePlugin({
            PRODUCTION: JSON.stringify(true),
			API: JSON.stringify("http://localhost:63200/"),
			HUB: JSON.stringify("http://localhost:63200/")
        })
    ]
});