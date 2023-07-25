
        // var form = document.getElementById("myform");
        // function handleForm(event) { event.preventDefault(); } 
        // form.addEventListener('submit', handleForm);


        function Login() {
            let form = document.forms['myform'];
            let username = form['username'].value;
            let password = form['username'].value;
            SendLoginRequest(username, password);
        }

        function GetCityByToken() {
            $.ajax({
                type: "GET",
                beforeSend: function (request) {
                    request.setRequestHeader("Authorization", "bearer " + token);
                },
                url: "https://localhost:7110/api/City/" + 1,                   ///// URL must be specified 
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (result) {
                    console.log(result);
                },
                error: function (ex) {
                    console.log(ex.responseText);
                }
            });
        }

        let token = "";
         
        function SendLoginRequest(username, password) {
            let obj = {
                username: username,
                password: password
            };

            $.ajax({
                type: "POST",
                url: "https://localhost:7110/api/Auth/Login",                   ///// URL must be specified 
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(obj), //this class returns a json object
                cache: false,
                success: function (result) {
                    token = result;
                    console.log(result);
                },
                error: function (ex) {
                    token = ex.responseText;
                    console.log(token);
                }
            });
        }
