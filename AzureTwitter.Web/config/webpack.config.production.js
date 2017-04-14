const webpack = require('webpack');
const webpackMerge = require('webpack-merge');

module.exports = webpackMerge(require('./webpack.config.common'), {
    plugins: [
        new webpack.DefinePlugin({
            PRODUCTION: JSON.stringify(false),
        })
    ]
});