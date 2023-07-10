import { Component } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public message: string | undefined;
  public messages: any[] = [];
  public tempUserName: string | undefined;
  public userName: string | undefined;

  private hubConnection: signalR.HubConnection | undefined;

  constructor() {
    this.startSignalRConnection();
  }

  private startSignalRConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7098/chat')
      .build();

    this.hubConnection.start()
      .then(() => console.log('SignalR connection started.'))
      .catch(err => console.log('Error while starting SignalR connection: ', err));

    this.hubConnection.on('ReceiveMessage', (message: any) => {
      console.log('Received message: ', message);
      this.messages.push(message);
    });
  }

  public startChat(): void {
    if (this.tempUserName) {
      this.userName = this.tempUserName;
      this.tempUserName = '';
    }
  }

  public sendMessage(): void {
    if (this.message) {
      const chatMessage = {
        sender: this.userName,
        content: this.message
      };
      this.hubConnection?.invoke('SendMessage', chatMessage)
        .catch(err => console.error('Error while sending message: ', err));

      this.message = '';
    }
  }
}
