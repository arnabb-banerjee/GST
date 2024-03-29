USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[EmailLog]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmailLog](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NULL,
	[MailOption] [varchar](50) NULL,
	[Subject] [varchar](500) NULL,
	[Body] [varchar](max) NULL,
	[TO] [varchar](max) NULL,
	[CC] [varchar](max) NULL,
	[ErrorMessage] [varchar](max) NULL,
	[InnerException] [varchar](max) NULL,
	[StackStress] [varchar](max) NULL,
	[EmailSentOn] [datetime] NULL,
	[FromEmail] [varchar](200) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[EmailLog] ON 

INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (1, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi </td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=0690d2b2-8f66-4874-a9ae-38cab629539a14-10-2018 16:14:54''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'abc2@gmail.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A979010C6030 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (2, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi </td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=90bd0d5a-32b4-4d49-89df-8d678bd13a5f21-10-2018 08:50:00''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'abc@gmal.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9800091AC83 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (3, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi </td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=2bb73552-1a9b-47c2-875a-66d6e306790621-10-2018 22:44:24''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'abc@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9800176DDD9 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (4, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi adada</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=7212f475-9b22-4d25-b96b-ad576f1b8f6805-11-2018 20:15:08''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'btechar@uweu.ui', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A98F014DDD57 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (5, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=4637fee4-0092-43d8-892a-8104838c5bb006-11-2018 07:16:23''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9900077F620 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (6, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=4e416070-4118-45b5-935d-1693b57ec06009-11-2018 19:49:14''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9930146BC0B AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (7, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=fa7227b9-58fb-46a2-9b66-4c40a252355c10-11-2018 22:23:59''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99401713C21 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (8, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=1d873692-9dc5-4afb-b4f5-140458830a7210-11-2018 22:25:02''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A994017185DA AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (9, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=6c1a4dca-df9e-4380-a34a-5d406528663210-11-2018 22:27:32''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A994017235C2 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (10, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=4ff1a3aa-5631-455c-8a65-5f631ea6e9a110-11-2018 22:28:00''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9940172563D AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (11, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=7b42c0fb-38f0-4ca5-bd66-7b682e8affb310-11-2018 22:28:35''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99401727F25 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (12, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=f4be7d78-92ca-493c-803e-e8e7a533515a10-11-2018 22:29:12''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9940172AA87 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (13, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=e5b593fb-d66e-43d2-b661-887bfab4775910-11-2018 22:32:08''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9940173797B AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (14, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=3d134576-27ae-47e5-b10a-aa23acdaf32410-11-2018 22:35:30''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99401746597 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (15, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=df4b1f71-9c2e-4bc0-a560-b2f2d19fd7d310-11-2018 23:00:23''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A994017B3B2A AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (16, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=590b3f39-cd0d-414f-985e-7c5d72e5d70110-11-2018 23:01:22''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A994017B811E AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30058, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi asdfsa</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=e3b77005-0924-4a84-ab61-6a1f0845ad1119-11-2019 18:21:07''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'sadfsda@asdfsa.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0A012E88EA AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30060, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=ee212679-37cc-42d2-9488-c7b86cceb7ab21-11-2019 17:56:50''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0C0127DC1E AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30059, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi asdfsa</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=9f5fb94f-6a01-4395-ab37-48665c1a29e821-11-2019 17:53:04''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'sadfsda@asdfsa.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0C0126D44B AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30061, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=299c0925-b1c2-45b0-8c97-f26938c6a7a721-11-2019 18:20:22''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0C012E52DB AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30062, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=21fbe692-2fa8-45b6-9d11-9df57f30b97c21-11-2019 18:32:55''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0C0131C6DA AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30063, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=de45733c-7ab4-46ca-a7cc-7049802ef04a21-11-2019 18:45:48''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0C0135508B AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30064, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=a118a694-622a-45a5-bea7-f182210d51e421-11-2019 18:48:40''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0C01361A2B AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30065, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=43cc2714-6cea-48bf-9b74-8a8d881b825a22-11-2019 19:37:36''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0D01438BCD AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30066, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Atanu</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=fe6f8dc2-60d6-47c4-8d99-38021f15a65722-11-2019 19:45:00''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'atanu@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0D0145936D AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30067, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=f21355e1-6ff9-4419-ba72-ac6c58def38c22-11-2019 19:49:11''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0D0146C42D AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30068, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=9d0a01a4-b2a6-42c4-a38d-c10b25e7e16923-11-2019 13:46:38''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E00E325A7 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30073, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=d6ac4d00-1e06-4f33-bad6-dcd02e74abe123-11-2019 14:01:54''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E00EEC2F4 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30069, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=15f18e04-bee9-4a8f-8fa3-752bc06790f023-11-2019 13:47:42''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E00E3716C AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30070, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=804decc0-eeb7-4b98-9762-706c84cdb64723-11-2019 13:50:20''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E00E42884 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30071, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi asdfsa</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=64bf0a94-cf63-4556-8738-8d314b6cdf9923-11-2019 13:50:54''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'sadfsda@asdfsa.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E00E4504A AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30072, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=e5b688f2-917b-42a5-8783-5e65eca0de7823-11-2019 13:51:56''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E00E49909 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (32, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=f3a3cb05-5ce0-46f9-b613-7d9de16a63d611-11-2018 00:49:08''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A995000D9828 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (34, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=19d080cb-1aff-4d46-bbd1-3450b18f370818-11-2018 17:41:34''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99C0123AB9C AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (33, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=1e8f5ee7-d75f-4c5a-890c-952d3f9d9d0611-11-2018 00:56:20''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A995000F92CE AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (35, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=7ded286d-f4c2-4b0d-aa31-e7de99a3847418-11-2018 17:42:20''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99C0123E0E0 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (37, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=c1598802-3285-4dde-ad24-7c007b0855ff18-11-2018 17:54:58''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99C012759C3 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (38, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=8a4d89ee-c72f-40fb-a506-71f92cd5047718-11-2018 18:00:35''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99C01290F78 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (39, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=7ef37785-b440-41c2-8aa4-ca544526d8c818-11-2018 18:03:24''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99C0129AABF AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=baf0faad-f3ee-4ea8-ad9e-5f3ecad7be6818-11-2018 18:21:21''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99C012E989A AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (41, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=24a3d3de-b621-4acd-b0bd-6f9b53db8b3f18-11-2018 18:22:29''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99C0130681A AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (42, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=baac2e79-586c-46db-be42-3852ca5161f718-11-2018 18:41:30''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99C013425D0 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (43, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=83f7052c-92fb-4f64-b7d2-b61d4a3d8c5318-11-2018 19:06:07''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99C013AE821 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (44, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=6b80c063-0fce-4000-8f17-632f5038a9c518-11-2018 19:22:41''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99C013FB999 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (45, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=f4fcdb8d-6b6f-4294-83c2-0c2bf2fbd71e18-11-2018 19:55:46''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99C0148C58B AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (10043, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi </td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=bc891792-4c6c-4620-b8ab-d7f76c8614f111-12-2018 16:41:37''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'arnab.baanerjee@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9B301133863 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (10044, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi </td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=6ee19bd5-9969-4563-a42c-2bcb0e12a0b211-12-2018 19:35:54''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'asd@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9B301431971 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (10045, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jhasgdjahgJHAGDAJ</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=29cde99f-53c8-4cf1-b663-6ff85911093b23-12-2018 17:39:43''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'AJSHSD@kjashka.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9BF012329A7 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (17, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=aba8ca99-3656-4477-b375-57a6eb17273510-11-2018 23:01:54''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A994017BA6D3 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (18, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=7706522e-b5e2-4d5e-a683-d2a566a7c4d010-11-2018 23:03:50''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A994017C2DBC AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (19, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=229f8f51-6c43-4a1e-b995-074c7b4ae3bc10-11-2018 23:05:29''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A994017CA28E AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (20, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=e75ef7b4-b7ac-4d26-a989-bca406d1bcee10-11-2018 23:06:01''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A994017CC851 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (21, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=805fd951-0b5d-43bc-9e89-364958907eca10-11-2018 23:08:28''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A994017D7405 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (28, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=f2d1456a-2875-4c06-9547-0f299316349a11-11-2018 00:23:35''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99500069370 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (22, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=50efc81a-6919-4266-b68a-2ac16557553810-11-2018 23:33:36''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99401845B94 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (23, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=f17bfed0-581a-4bea-a739-740c79f5e4ee10-11-2018 23:33:58''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9940184748B AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (24, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=f09c8216-3d8f-4b75-810d-e7337fa9797e10-11-2018 23:34:19''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99401848DAE AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (25, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=65b9ff6d-2d4d-4cc1-a625-ca9e0795a0ad10-11-2018 23:34:47''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9940184AE48 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (26, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=d6d6d17f-f39a-4fe5-8a8f-9bfb4a072e9e10-11-2018 23:36:26''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99401852245 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (27, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=b9fe8531-4240-43fe-84a3-c56a29678ce010-11-2018 23:40:37''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99401864802 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (29, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=175136f1-98bb-485e-883e-778d1f6f32c211-11-2018 00:24:28''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9950006D20D AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=52d99e4e-f7c9-46a3-8782-45d70f2395fe11-11-2018 00:29:57''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A995000853C1 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (31, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=201659da-84a9-48f9-9d53-d2c3babc528d11-11-2018 00:48:33''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A995000D6F9E AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (36, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=418db971-f68b-4cfd-ad31-814742f78de018-11-2018 17:51:45''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A99C012676A0 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (10046, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi jgjhgj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=41724d4e-52e2-4554-a798-2e62ea09beae25-12-2018 09:01:59''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'hgfhgf@jjhg.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9C10094F623 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (10047, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi ars</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=48c8b1fa-ea76-4d56-a917-402c2409e74025-12-2018 09:16:29''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'user1@gmail.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9C10098F0C7 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (10048, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi ars</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=5f754062-eee4-4540-a5e5-ab7a5def253325-12-2018 09:18:04''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'user1@gmail.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9C1009960EC AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (10049, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi kasdfhalk</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=696596c7-19f1-416f-96ae-7477cee155aa26-12-2018 00:14:45''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'kaskdfjhs@alksfjs.co', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9C200042C66 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (10050, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi kj</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=7d29c9e0-88bc-415f-a733-75d424f1621a26-12-2018 01:39:48''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'kajdhka@adskfjsla.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9C2001B8458 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (20050, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi indra123industry</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=75094d95-37be-4db7-aae1-c1739f67cd9a29-12-2018 07:24:30''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'indra123industry@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000A9C5007A3195 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (20051, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi gkjh</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=faea557e-8c25-4cac-967e-7678944650bc27-10-2019 07:49:37''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'jhg@afsf.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AAF3008116C4 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (20052, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi asfs</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=d61e609c-d0c5-45fa-8a26-e58c481a930627-10-2019 15:22:53''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'asfsad@Wassaf.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AAF300FD93F8 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (20053, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi asfsa</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=92bf8f69-baf2-4ff4-8811-cfccf463e23a27-10-2019 15:34:40''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'asdfsa@asdfdsa.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AAF30100D07C AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (20054, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi sdgdsf</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=b39209a2-9d1a-4788-abc7-173f32bf6bd427-10-2019 15:51:16''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'sdfgfdsg@asfsafs.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AAF301055F3A AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (20055, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi ooiyuo</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=17432ba4-ef4b-4000-9114-82b73be5c8ed27-10-2019 16:06:36''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'aggfds@kjhgk.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AAF3010995C3 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (20058, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Sandip</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=a3303b92-c225-4c49-861e-d2003b0c36f518-11-2019 13:35:04''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'sandip@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0900DFF75C AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (20056, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi asdfdsafa</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=af1c0f6c-4d0e-4c2f-b8c2-98aa31b2fcf127-10-2019 16:26:05''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'asdfsdfa@laksdhlsak.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AAF3010EEF33 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (20057, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi laskjhfsalk</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=f5189dbd-00ba-43b9-897a-a1d09c5ea1ef28-10-2019 17:47:19''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'alskdjfhsadl@alskdjfhsakl.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AAF401253F11 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (20059, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Sandip</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=4d411c26-5b75-475c-8b23-593942b348b318-11-2019 13:37:18''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'sandip@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0900E093B4 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (20060, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Sandip</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=09d77816-8a15-41d0-b0ca-01d88ed2e33f18-11-2019 13:42:11''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'sandip@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0900E1EB6D AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30074, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=410a8850-0319-4758-b012-538027ba548023-11-2019 14:38:16''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E00F15589 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30075, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=990764f2-481a-4161-af9d-d7027df1bc1a23-11-2019 14:40:01''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E00F1D059 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30076, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=8f98b071-50a1-4ed8-9138-6c669c7b03a123-11-2019 14:45:15''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E00F33D92 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30077, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=bb9cab48-b3ad-4a24-b859-816df1e8b3ab23-11-2019 15:27:46''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E00FEEB54 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30078, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=1e70f320-0a5b-4316-8ff0-3c413c7c42d323-11-2019 15:42:36''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'jooyy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E0102FEBE AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30079, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=8d88d9c1-f890-49e2-bb50-56ffec9552c423-11-2019 15:43:01''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'jooyy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E01031AF4 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30080, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=c073a5da-9aa9-47e3-842d-cb6e26215a3e23-11-2019 15:43:41''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'jooyy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E010349CA AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40077, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=20751528-63fd-4d71-9fa9-e50eb0ed852223-11-2019 16:30:06''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E01100A8C AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (30081, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=ac7d73dc-bc9d-4116-85f1-928a9c447ec823-11-2019 16:06:39''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E010998A3 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40078, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=e0f4ee76-62d0-463d-8863-14f747cb7ef523-11-2019 16:31:16''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E01105B66 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40079, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=ee44ae65-58da-4a49-bb38-9f72c77d70e823-11-2019 16:42:58''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E011392D7 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40080, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi asdfdsafa</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=2ed0b46f-73f1-4865-b787-2e0e2d7995f823-11-2019 16:45:49''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'asdfsdfa@laksdhlsak.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E0114B7B0 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40081, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=1ead1dd9-f61f-47e7-b074-f9496daf236923-11-2019 17:29:56''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E0120795A AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40082, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=f15d522b-a52c-41c2-8680-9e89cae7bd8423-11-2019 17:31:46''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E0120F9F6 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40083, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=900bf149-aa04-4ce6-b523-8d60a068c22823-11-2019 17:34:41''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E0121C699 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40084, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=5b6ca149-ae8a-4f6f-be93-aa29f9dfdbc523-11-2019 17:37:41''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0E01229A42 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40085, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Sandip</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=bb31cb0a-e203-4898-9553-dfb4bf30e65e24-11-2019 16:18:07''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'sandip@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB0F010CBFD1 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40086, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi sadfsadf</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=809ac107-5724-4a50-9d1d-c2baf25d6f0626-11-2019 22:32:36''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'adfsdafsadf@sadfsdaf.sadfasda', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1101739AFB AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40087, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab </td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=9a82a049-0d08-44c6-af8c-e70a7ec4cf2f26-11-2019 22:47:41''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'aaa@aaa.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB110177BEF6 AS DateTime), N'info@bigbook.io')
GO
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40088, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Atanu</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=3b253395-ce65-4fe0-b1c6-598348f77dce30-11-2019 01:04:13''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'atanu123@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB150011BD15 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40089, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi sdfgdsfg</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=6a3e8ed7-b950-4472-8d2f-da4637fbbd5f04-12-2019 01:50:41''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'sdgsdfgdfs@adsfsadfs.dskfsa', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB19001E8156 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40092, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Biplab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=dbe7348d-1b83-4302-a7c4-ed79f40fc27206-12-2019 00:01:21''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'biplab@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1B000078C9 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40090, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi John</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=35c17994-cfbf-4693-abc0-f28067a2721705-12-2019 21:43:15''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'john@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1A01660D5E AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40091, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi John</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=9bec39df-c1de-44d2-adb0-3ca293d71d9805-12-2019 21:43:50''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'john@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1A01663506 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40093, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi adada</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=b728c77a-a1fe-4ac9-896c-d217e40db42a06-12-2019 00:02:25''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'btechar@uweu.ui', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1B0000C3B8 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40094, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=a6629e8f-a78d-4332-8425-1607cd983d4a06-12-2019 00:04:16''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1B000145C1 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40095, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=b240ecf1-efbb-46e2-8777-cd59b8c708a806-12-2019 01:00:26''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1B0010B315 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40096, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Joy</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=e7f0b21c-22f0-4b6e-85b6-5173b9387fda08-12-2019 00:13:03''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'joy123@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1D0003B055 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40097, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Shouvik</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=3db77717-8314-4c23-921d-bcdaa6318dbe08-12-2019 00:23:33''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'sauvik@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1D000691D8 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40098, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Shouvik</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=217a0177-e4b8-473c-bcb3-7908a0ada02b08-12-2019 00:29:38''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'sauvik@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1D00083D8A AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40099, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi abc</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=4c5a9e7c-e440-4dc6-a955-ee2b64f21c4208-12-2019 12:18:02''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'abc123@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1D00CACE99 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40100, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=752d5d3e-30bb-4c0e-834b-a1c985bbdf1c08-12-2019 12:52:13''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'atanu1@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1D00D432A6 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40101, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi abc</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=a944f3c3-7b03-42cf-a69b-544a4a5d957408-12-2019 12:53:05''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'abc123@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1D00D46F6A AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40102, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Arnab</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=ad52c9cb-d302-4325-9505-8a9845a4760008-12-2019 13:04:31''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'Arnab@arnab.co.in', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1D00D7933C AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40103, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=08d708de-c492-4482-bd66-9e9d13cc795f08-12-2019 13:06:28''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya1@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1D00D81CB3 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40104, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=6b55b832-43ed-44d9-8191-a150ffe04dcb08-12-2019 13:07:21''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'atanu1@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1D00D85AC5 AS DateTime), N'info@bigbook.io')
INSERT [dbo].[EmailLog] ([ID], [UserID], [MailOption], [Subject], [Body], [TO], [CC], [ErrorMessage], [InnerException], [StackStress], [EmailSentOn], [FromEmail]) VALUES (40105, 0, N'EmailConfirmationOnRegistration', N'Email verification for your BigPage account', N'<table>
  <tr><td>Hi Nirmalya</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>
  <tr><td>Please click on the <a href=''http://app.bigpage.io/activateregaccount?accountkey=b94bb471-dd2f-42af-822d-a06e725f3bd008-12-2019 13:09:43''>link</a> to activate your account</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>Regards</td></tr>
  <tr><td><a href=''http://app.bigpage.io/''>Big page</a></td></tr>
</table>
', N'nirmalya1@gmail.com', N'', N'Failure sending mail.', N'System.Net.WebException: Unable to connect to the remote server ---> System.Net.Sockets.SocketException: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 127.0.0.0:21
   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.ServicePoint.ConnectSocketInternal(Boolean connectFailure, Socket s4, Socket s6, Socket& socket, IPAddress& address, ConnectSocketState state, IAsyncResult asyncResult, Exception& exception)
   --- End of inner exception stack trace ---
   at System.Net.ServicePoint.GetConnection(PooledStream PooledStream, Object owner, Boolean async, IPAddress& address, Socket& abortSocket, Socket& abortSocket6)
   at System.Net.PooledStream.Activate(Object owningObject, Boolean async, GeneralAsyncDelegate asyncCallback)
   at System.Net.PooledStream.Activate(Object owningObject, GeneralAsyncDelegate asyncCallback)
   at System.Net.ConnectionPool.GetConnection(Object owningObject, GeneralAsyncDelegate asyncCallback, Int32 creationTimeout)
   at System.Net.Mail.SmtpConnection.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpTransport.GetConnection(ServicePoint servicePoint)
   at System.Net.Mail.SmtpClient.GetConnection()
   at System.Net.Mail.SmtpClient.Send(MailMessage message)', N'   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at Databaselayer.MailHelper..ctor(String MailOption, String Subject, String Body, String FromEmailID, String FromName, String ToList, String ToDisplayList, String CCList, String CCDisplayList) in C:\Arnab\Project\iGST\iGST_Svc\MailHelper.cs:line 84', CAST(0x0000AB1D00D90662 AS DateTime), N'info@bigbook.io')
SET IDENTITY_INSERT [dbo].[EmailLog] OFF
