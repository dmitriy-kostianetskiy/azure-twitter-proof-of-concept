const webpack = require('webpack');
const path = require('path');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

const rootPath = path.resolve(__dirname, '..');
const appPath = path.join(rootPath, 'app');
const componentPath = path.join(appPath, 'components');
const outputPath = path.join(rootPath, 'wwwroot/dist');

const extract = new ExtractTextPlugin({
    filename: '[name].bundle.css',
    disable: false,
    allChunks: true
});

module.exports = {
	devServer: {
		compress: true,
		hot: true,
        publicPath: rootPath
	},
    entry: {
        'polyfills': require('./entries/polyfills'),
        'vendor': require('./entries/vendor'),
        'app': appPath
    },
    output: {
        path: outputPath,
        filename: '[name].bundle.js',
        sourceMapFilename: '[file].map'
    },
    module: {  
		exprContextCritical: false,
        rules: [
            { 
                test: /\.scss$/,
                exclude: [componentPath], 
                use: extract.extract({
                    fallback: 'style-loader', 
                    use: 'css-loader!resolve-url-loader!sass-loader?sourceMap'
                })
            },
            { 
                test: /\.css$/, 
                exclude: [componentPath], 
                use: extract.extract({
                    fallback: 'style-loader', 
                    use: 'css-loader'
                }) 
            },
            { 
                test: /\.ts$/, 
                use: [
                    'awesome-typescript-loader', 
                    'angular2-template-loader'
                ] 
            },
            { 
                test: /\.scss$/, 
                include: [componentPath], 
                use: [
                    'raw-loader', 
                    'sass-loader'
                ] 
            },
            { 
                test: /\.html$/, 
                include: [appPath], 
                use: ['raw-loader']
            },
            { 
                test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9]|\?[\w]*)?$/, 
                use: [ 
                    {
                        loader: 'file-loader',
                        options: {
                            name: 'fonts/[name].[hash].[ext]'
                        }
                    }
                ]
            },
            { 
                test: /\.(ttf|eot|svg)(\?v=[0-9]\.[0-9]\.[0-9]|\?[\w]*)?$/, 
                use: [ 
                    {
                        loader: 'file-loader',
                        options: {
                            name: 'fonts/[name].[hash].[ext]'
                        }
                    }
                ]
            },
            { 
                test: /\.(png|jpe?g|gif|ico)$/, 
                use: [ 
                    {
                        loader: 'file-loader',
                        options: {
                            name: 'images/[name].[hash].[ext]'
                        }
                    }
                ]
            }
        ]
    },
    resolve: {
        modules: [
            path.resolve(rootPath, 'src/app/'),
            'node_modules'
        ],
		extensions: ['.js', '.json', '.ts'],
		alias: {
			// components: path.join(appPath, 'components'),
			// actions: path.join(appPath, 'actions'),
			// definitions: path.join(appPath, 'definitions'),
			// models: path.join(appPath, 'models'),
			// reducers: path.join(appPath, 'reducers'),
			// stores: path.join(appPath, 'stores'),
		}
    },
    plugins: [
        new webpack.optimize.CommonsChunkPlugin({ name: ['app', 'vendor', 'polyfills'], minChunks: Infinity }),
        extract
    ]
};