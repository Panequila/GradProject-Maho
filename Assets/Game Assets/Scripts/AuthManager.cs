//using System.Collections;
//using UnityEngine;
//using Firebase;
//using Firebase.Auth;
//using TMPro;
//using Mirror;
//using UnityEngine.SceneManagement;

//public class AuthManager : NetworkManager
//{
//    //Firebase variables
//    [Header("Firebase")]
//    public DependencyStatus dependencyStatus;
//    public FirebaseAuth auth;
//    public FirebaseUser User;

//    //Login variables
//    [Header("Login")]
//    public TMP_InputField emailLoginField;
//    public TMP_InputField passwordLoginField;
//    public TMP_Text warningLoginText;
//    public TMP_Text confirmLoginText;

//    //Register variables
//    [Header("Register")]
//    public TMP_InputField usernameRegisterField;
//    public TMP_InputField emailRegisterField;
//    public TMP_InputField passwordRegisterField;
//    public TMP_InputField passwordRegisterVerifyField;
//    public TMP_Text warningRegisterText;

//    //public GameObject playerPrefab;

//    void Awake()
//    {
//        //DontDestroyOnLoad(this.gameObject);

//        //Check that all of the necessary dependencies for Firebase are present on the system
//        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
//        {
//            dependencyStatus = task.Result;
//            if (dependencyStatus == DependencyStatus.Available)
//            {
//                //If they are avalible Initialize Firebase
//                InitializeFirebase();
//            }
//            else
//            {
//                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
//            }
//        });
//    }

//    private void InitializeFirebase()
//    {
//        Debug.Log("Setting up Firebase Auth");
//        //Set the authentication instance object
//        auth = FirebaseAuth.DefaultInstance;
//    }

//    //Function for the login button
//    public void LoginButton()
//    {
//        Debug.Log("Das 3la Login 1");
//        //Call the login coroutine passing the email and password
//        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
//        Debug.Log("Das 3la Login 2");
//    }
//    //Function for the register button
//    public void RegisterButton()
//    {
//        //Call the register coroutine passing the email, password, and username
//        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
//    }

//    public IEnumerator Login(string _email, string _password)
//    {
//        //Call the Firebase auth signin function passing the email and password
//        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
//        //Wait until the task completes
//        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

//        if (LoginTask.Exception != null)
//        {
//            //If there are errors handle them
//            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
//            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
//            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

//            string message = "Login Failed!";
//            switch (errorCode)
//            {
//                case AuthError.MissingEmail:
//                    message = "Missing Email";
//                    break;
//                case AuthError.MissingPassword:
//                    message = "Missing Password";
//                    break;
//                case AuthError.WrongPassword:
//                    message = "Wrong Password";
//                    break;
//                case AuthError.InvalidEmail:
//                    message = "Invalid Email";
//                    break;
//                case AuthError.UserNotFound:
//                    message = "Account does not exist";
//                    break;
//            }
//            warningLoginText.text = message;
//        }
//        else
//        {
//            //User is now logged in
//            //Now get the result
//            User = LoginTask.Result;
//            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
//            warningLoginText.text = "";
//            confirmLoginText.text = "Logged In";

//            //ClientScene.RegisterPrefab(playerPrefab);

//            //Debug.Log("Scene Loaded");

//            //SceneManager.LoadScene("Maho"); //local
//            //NetworkManager.singleton.ServerChangeScene("Maho"); //mirror
//            //Debug.Log(NetworkManager.networkSceneName);
//            //SceneManager.LoadSceneAsync(subScene, LoadSceneMode.Additive);

//            //StartCoroutine(ActiveScene());

//            //Debug.Log(NetworkManager.networkSceneName);
//            //Scene currentScene = SceneManager.GetActiveScene();
//            //string sceneName = currentScene.name;

//            //NetworkManager.singleton.ServerChangeScene("Maho");
//            //if (NetworkManager.networkSceneName == "Maho")
//            //{
//            //StartClient();


//            //    Debug.Log("ALo");

//            //}
//            ////NetworkManager.
//            //Debug.Log(NetworkManager.networkSceneName);
//            //Debug.Log("The Server Started");



//        }
//    }

//    //IEnumerator ActiveScene()
//    //{
//    //    while (true)
//    //    {
//    //        Scene currentScene = SceneManager.GetActiveScene();
//    //        string sceneName = currentScene.name;
//    //        Debug.Log(sceneName);
//    //        if (sceneName == "Maho")
//    //        {
//    //            //StartHost();
//    //            //yield break;
//    //            Debug.Log("Scene Name: " + sceneName);
//    //        }
//    //        Debug.Log("Inside the Coroutine");

//    //        yield return new WaitForSeconds(5f);

//    //    }

//    //}


//    public override void OnStartServer()
//    {
//        //SceneManager.LoadScene("Maho");
//        //ServerChangeScene("Maho"); 
//        //StartCoroutine(WaitAndPrint());
//        base.OnStartServer();
//        NetworkServer.RegisterHandler<CreateMMOCharacterMessage>(OnCreateCharacter);


//    }

//    public override void OnStartHost()
//    {
//        base.OnStartHost();
//    }

//    public struct CreateMMOCharacterMessage : NetworkMessage
//    {
//        public string name;
//    }

//    void OnCreateCharacter(NetworkConnection conn, CreateMMOCharacterMessage message)
//    {
//        // playerPrefab is the one assigned in the inspector in Network
//        // Manager but you can use different prefabs per race for example
//        GameObject gameobject = Instantiate(playerPrefab);

//        // Apply data from the message however appropriate for your game
//        // Typically Player would be a component you write with syncvars or properties

//        // call this to use this gameobject as the primary controller
//        NetworkServer.AddPlayerForConnection(conn, gameobject);
//    }

//    public override void OnClientConnect(NetworkConnection conn)
//    {
//        SceneManager.LoadScene("Maho");

//        base.OnClientConnect(conn);

//        // you can send the message here, or wherever else you want
//        CreateMMOCharacterMessage characterMessage = new CreateMMOCharacterMessage
//        {
//            name = "Joe Gaba Gaba",
//        };

//        conn.Send(characterMessage);
//    }

//    private IEnumerator Register(string _email, string _password, string _username)
//    {
//        if (_username == "")
//        {
//            //If the username field is blank show a warning
//            warningRegisterText.text = "Missing Username";
//        }
//        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
//        {
//            //If the password does not match show a warning
//            warningRegisterText.text = "Password Does Not Match!";
//        }
//        else
//        {
//            //Call the Firebase auth signin function passing the email and password
//            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
//            //Wait until the task completes
//            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

//            if (RegisterTask.Exception != null)
//            {
//                //If there are errors handle them
//                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
//                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
//                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

//                string message = "Register Failed!";
//                switch (errorCode)
//                {
//                    case AuthError.MissingEmail:
//                        message = "Missing Email";
//                        break;
//                    case AuthError.MissingPassword:
//                        message = "Missing Password";
//                        break;
//                    case AuthError.WeakPassword:
//                        message = "Weak Password";
//                        break;
//                    case AuthError.EmailAlreadyInUse:
//                        message = "Email Already In Use";
//                        break;
//                }
//                warningRegisterText.text = message;
//            }
//            else
//            {
//                //User has now been created
//                //Now get the result
//                User = RegisterTask.Result;

//                if (User != null)
//                {
//                    //Create a user profile and set the username
//                    UserProfile profile = new UserProfile { DisplayName = _username };

//                    //Call the Firebase auth update user profile function passing the profile with the username
//                    var ProfileTask = User.UpdateUserProfileAsync(profile);
//                    //Wait until the task completes
//                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

//                    if (ProfileTask.Exception != null)
//                    {
//                        //If there are errors handle them
//                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
//                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
//                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
//                        warningRegisterText.text = "Username Set Failed!";
//                    }
//                    else
//                    {
//                        //Username is now set
//                        //Now return to login screen
//                        UIManager.instance.LoginScreen();
//                        warningRegisterText.text = "";
//                    }
//                }
//            }
//        }
//    }
//}
