// config SDK
const firebaseConfig = {
    apiKey: "AIzaSyCz01zlwSeLQv9xZikTDd0FxMbHkSLNR3Q",
    authDomain: "financialplannerapi.firebaseapp.com",
    databaseURL: "https://financialplannerapi-default-rtdb.firebaseio.com",
    projectId: "financialplannerapi",
    storageBucket: "financialplannerapi.appspot.com",
    messagingSenderId: "216295449023",
    appId: "1:216295449023:web:7ee4c554a9f6e1b08e010a"
  };

 
   // Inicializando o Firebase
   const firebaseApp = firebase.initializeApp(firebaseConfig);
   const database = firebase.database();

   
   // Função para verificar se o usuário já está cadastrado
function checkIfUserExists(email) {
    return firebase.database().ref('users').orderByChild('email').equalTo(email).once('value')
        .then(function(snapshot) {
            return snapshot.exists();
        })
        .catch(function(error) {
            console.error("Erro ao verificar usuário: ", error);
            return false;
        });
}

// Função JavaScript para salvar o usuário no Firebase Realtime Database
function saveUserToRealtimeDatabase(name, email) {
    // Converter o email para minúsculo
    email = email.toLowerCase();

    // Verifica se o email já existe
    checkIfUserExists(email).then(function(exists) {
        if (exists) {
            console.log("Usuário com este e-mail já está cadastrado.");
            alert("Este e-mail já está cadastrado. Por favor, use outro e-mail.");
        } else {
            // Se o email não existe, salva o novo usuário
            const database = firebase.database();
            const newUserRef = database.ref('users').push();

            // Obtém a data atual
            const currentDate = new Date().toISOString();

            // Obtém a cidade do usuário usando o ipinfo.io
            fetch('https://ipinfo.io/json?token=2e337157c07dd1')
                .then(response => response.json())
                .then(data => {
                    const cidade = data.city || "Cidade desconhecida"; // Captura a cidade ou define um valor padrão

                    // Salva o usuário no Firebase com as informações de cidade
                    newUserRef.set({
                        name: name,
                        email: email,
                        city: cidade,       // Adiciona a cidade do usuário
                        registeredAt: currentDate // Adiciona a data de registro
                    }).then(() => {
                        console.log("Usuário salvo com sucesso no Realtime Database.");
                    }).catch((error) => {
                        console.error("Erro ao salvar usuário: ", error);
                    });
                })
                .catch((error) => {
                    console.error("Erro ao obter localização do usuário: ", error);
                });
        }
    }).catch(function(error) {
        console.error("Erro ao verificar ou salvar usuário: ", error);
    });
}

