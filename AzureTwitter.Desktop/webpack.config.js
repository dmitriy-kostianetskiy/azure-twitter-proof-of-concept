/// <binding ProjectOpened='Watch - Development' />
var env = process.env.NODE_ENV || 'development';
module.exports = require('./config/webpack.config.' + env);