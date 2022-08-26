import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Toast, ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private hubConnection!: HubConnection;
  constructor(private toastr: ToastrService,private router:Router) { }
  public subscribe = () => {
    const options = {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
    };

    this.hubConnection = new HubConnectionBuilder()
        .withUrl(environment.notificationsApi + 'Notifications?token='+localStorage.getItem('token'), options)
        .build();

    this.hubConnection
        .start()
        .then(() => console.log('Connection started'))
        .catch(err => console.log('Error while starting connection: ' + err));

    this.hubConnection.on('ShowNotification', (data) => {
        console.log(data);
        this.toastr.success(`${data.senderName} posted ${data.title}!!!`, "New Post!").onTap.subscribe(()=>this.router.navigate(["/post/"+data.id]));
    });
  }
}
