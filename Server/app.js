
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

body1 = [];
body1['kinect'] = '';
body1['oculus'] = '';
body1['leap'] = '';

app.get('/', routes.index);

app.post('/kinect', function(req, res) {
	// TODO: Post data in MongoDB server

	var data = req.body.data;
	body1['kinect'] = data;
	console.log(body1['kinect']);

	res.send(body1['kinect']);
});

app.post('/oculus', function(req, res) {
	// TODO: Post data in MongoDB server

	var name = req.body.data;
	console.log(name);

	var data = req.body.data;
	body1['oculus'] = data;
	console.log(data);

	res.send(body1['kinect']);
});

app.post('/leap/left', function(req, res) {
	// TODO: Post data in MongoDB server

	res.setHeader('Content-Type', 'application/json');
	res.end(JSON.stringify({
		left_arm: 1,
		right_arm: 2,
		left_leg: 3,
		right_leg: 2,
		left_hand: 4,
		right_hand: 5
	}));
});

app.post('/leap/right', function(req, res) {
	// TODO: Post data in MongoDB server

	res.setHeader('Content-Type', 'application/json');
	res.end(JSON.stringify({
		left_arm: 1,
		right_arm: 2,
		left_leg: 3,
		right_leg: 2,
		left_hand: 4,
		right_hand: 5
	}));
});

app.get('/kinect/latest', function(req, res){
  res.setHeader('Content-Type', 'application/json');
  res.end(JSON.stringify({
  	left_arm: 1,
  	right_arm: 2,
  	left_leg: 3,
  	right_leg: 2,
  	left_hand: 4,
  	right_hand: 5
  }));
});

// app.get('/player', function(req, res){
//   res.setHeader('Content-Type', 'application/json');
//   res.end(JSON.stringify({
//   	id: 44,
//   	position: {
//   		x: 4,
//   		y: 8
//   	},
//   }));
// });

http.createServer(app).listen(app.get('port'), function(){
  console.log('Virtual self listening on port ' + app.get('port'));
});
