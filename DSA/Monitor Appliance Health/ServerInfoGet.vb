'https://<your server>:8081/sapiws/ServerInfoGet.
Dim client = New RestClient("https://<your server>:8081")
Dim request = New RestRequest("sapiws/ServerInfoGet", Method.POST)
Dim response As IRestResponse = client.Execute(request)
Dim content = response.Content Debug.WriteLine(content)
