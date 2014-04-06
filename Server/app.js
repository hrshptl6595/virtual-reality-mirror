
/**
 * Module dependencies.
 */

var express = require('express');
var routes = require('./routes');
var user = require('./routes/user');
var http = require('http');
var path = require('path');

// var mongoose = require('mongoose');
// mongoose.connect('mongodb://localhost:3000/bitcamp');

var app = express();

// all environments
app.set('port', process.env.PORT || 3000);
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');
app.use(express.favicon());
app.use(express.logger('dev'));
app.use(express.json());
app.use(express.urlencoded());
app.use(express.methodOverride());
app.use(app.router);
app.use(require('stylus').middleware(path.join(__dirname, 'public')));
app.use(express.static(path.join(__dirname, 'public')));

// development only
if ('development' == app.get('env')) {
  app.use(express.errorHandler());
}

body = [];
for (var i = 0; i < 7; i++) {
	body[i] = [];	

	body[i]['kinect']     = '';
	body[i]['oculus']     = '';
	body[i]['leap_right'] = '';
}

app.get('/', routes.index);

app.post('/kinect', function(req, res) {
	// TODO: Post data in MongoDB server

	var data = req.body.data;

	var bodyLine = data.substring(0, data.indexOf("\n"));
	var bodyData = bodyLine.split(":");
	var bodyIndex = bodyData[1];

	// console.log('bodyIndex: ' + bodyIndex);

	body[bodyIndex]['kinect'] = data;
	// console.log(body[bodyIndex]['kinect']);

	var response = '';
	console.log(response);
	for (var i = 0; i < 7; i++) {
		response += body[i]['kinect']     + "\n";
		response += body[i]['oculus']     + "\n";
		response += body[i]['leap_right'] + "\n";
	}
	// response += body[bodyIndex]['oculus'] + "\n" + body[bodyIndex]['leap_right'];
	res.send(response);
});

app.post('/oculus', function(req, res) {
	// TODO: Post data in MongoDB server

	var data = req.body.data;

	var bodyLine = data.substring(0, data.indexOf("\n"));
	var bodyData = bodyLine.split(":");
	var bodyIndex = bodyData[1];

	// console.log('bodyIndex: ' + bodyIndex);

	body[bodyIndex]['oculus'] = data;
	// console.log(body[bodyIndex]['oculus']);

	var response = '';
	console.log(response);
	for (var i = 0; i < 7; i++) {
		response += body[i]['kinect']     + "\n";
		response += body[i]['oculus']     + "\n";
		response += body[i]['leap_right'] + "\n";
	}
	// response += body[bodyIndex]['oculus'] + "\n" + body[bodyIndex]['leap_right'];
	res.send(response);
});

app.post('/leap/right', function(req, res) {
	// TODO: Post data in MongoDB server

	var data = req.body.data;

	var bodyLine = data.substring(0, data.indexOf("\n"));
	var bodyData = bodyLine.split(":");
	var bodyIndex = bodyData[1];

	// console.log('bodyIndex: ' + bodyIndex);

	body[bodyIndex]['leap_right'] = data;
	// console.log(body[bodyIndex]['leap_right']);

	var response = '';
	console.log(response);
	for (var i = 0; i < 7; i++) {
		response += body[i]['kinect']     + "\n";
		response += body[i]['oculus']     + "\n";
		response += body[i]['leap_right'] + "\n";
	}
	// response += body[bodyIndex]['oculus'] + "\n" + body[bodyIndex]['leap_right'];
	res.send(response);
});

app.get('/status', function(req, res) {
	// TODO: Post data in MongoDB server

	var response = '';
	for (var i = 0; i < 7; i++) {
		response += body[i]['kinect']     + "\n";
		response += body[i]['oculus']     + "\n";
		response += body[i]['leap_right'] + "\n";
	}
	// response += body[bodyIndex]['oculus'] + "\n" + body[bodyIndex]['leap_right'];
	res.send(response);
});

http.createServer(app).listen(app.get('port'), function(){
  console.log('Virtual self listening on port ' + app.get('port'));
});
