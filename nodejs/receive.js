var args = process.argv.slice(2)
var queuename = args[0]

var open = require('amqplib').connect('amqp://krvarma:var753ma@ubuntuserver');

// Consumer
open.then(function(conn) {
    return conn.createChannel();
  }).then(function(ch) {
    return ch.assertQueue(queuename).then(function(ok) {
      return ch.consume(queuename, function(msg) {
        if (msg !== null) {
          console.log(msg.content.toString());
          ch.ack(msg);
        }
      });
    });
  }).catch(console.warn);
