import React, { Component } from 'react';
//const { ServiceBusClient, ServiceBusMessage } = require("@azure/service-bus");

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { sbMessage: "Message" };
        this.azureMessageHandler = this.azureMessageHandler.bind(this);
    }

    azureMessageHandler() {
        const connString = "Endpoint=sb://jakejakejakehello.servicebus.windows.net/;SharedAccessKeyName=ErolsPolicy;SharedAccessKey=d8Qx9bYC8OVE0UqTalfZwzVr9deI1xi3IFTXJw9rGqE=;EntityPath=erolqueue";
        const queueName = "erolqueue";

        

        const msjHandler = async (msj) => {
            this.setState({ sbMessage: "Something" });
        }
    }

  render () {
    return (
      <div>
        <h1>Hello, Matthew!</h1>
            <p aria-live="polite">Message: <strong>{this.state.sbMessage}</strong></p>
        <button className="btn btn-primary" onClick={this.azureMessageHandler}>Get The Bus</button>
      </div>
    );
  }
}
