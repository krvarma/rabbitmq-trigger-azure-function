var amqp = require('amqplib');

var args = process.argv.slice(2)
var queuename = args[0]
var temperature = args[1]

amqp.connect('amqp://krvarma:var753ma@ubuntuserver').then(function(conn) {
  return conn.createChannel().then(function(ch) {
    var msg = 'Hello World, from RabbitMQ';

    var ok = ch.assertQueue(queuename, {durable: false});

    return ok.then(function(_qok) {
      var maxMessages = 1;
      
      for(i=0; i<maxMessages; ++i){
        msg = {
          deviceid: "123456789",
          temperature: temperature
        }

        ch.sendToQueue(queuename, Buffer.from(JSON.stringify(msg)));
      }

      console.log(" [x] Sent '%s'", msg);
      return ch.close();
    });
  }).finally(function() { conn.close(); });
}).catch(console.warn)