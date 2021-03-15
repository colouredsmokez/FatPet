using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject tutorial;

    public GameObject home;
    public GameObject bars;

    public GameObject pet;
    public GameObject emptyPet;

    public GameObject[] rabbitThinList;
    public GameObject[] hamsterThinList;
    public GameObject[] puppyThinList;
    public GameObject[] catThinList;

    public GameObject[] rabbitNormList;
    public GameObject[] hamsterNormList;
    public GameObject[] puppyNormList;
    public GameObject[] catNormList;

    public GameObject[] rabbitFatList;
    public GameObject[] hamsterFatList;
    public GameObject[] puppyFatList;
    public GameObject[] catFatList;

    public GameObject blankPanel;

    public GameObject[] thinPanels;
    public GameObject[] normPanels;
    public GameObject[] fatPanels;

    public GameObject startPanel;
    public Text userText;

    public GameObject signupPanel;
    public GameObject fullNameInput;
    public GameObject emailInput;
    public GameObject passwordInput;
    public GameObject passwordConfirmInput;

    public GameObject verifyEmailPanel;

    public GameObject loginPanel;
    public GameObject loginEmailInput;
    public GameObject loginPasswordInput;
    public Toggle remember;

    public GameObject emptyPanel;

    public GameObject loadingPanel;
    public Text loadingText;
    public GameObject beginButton;
    public GameObject againButton;

    public GameObject userDetailsPanel;
    public GameObject nameInput;
    public GameObject ageInput;
    public Toggle isM;
    public Toggle isF;

    public GameObject heightWeightPanel;
    public GameObject heightInput;
    public GameObject weightInput;

    public GameObject activityLevelPanel;
    public Toggle Lv1;
    public Toggle Lv2;
    public Toggle Lv3;
    public Toggle Lv4;
    public Toggle Lv5;

    public GameObject weightGoalPanel;
    public GameObject restartPanel1;
    public Toggle extrLoss;
    public Toggle normLoss;
    public Toggle mildLoss;
    public Toggle maintain;
    public Toggle mildGain;
    public Toggle normGain;
    public Toggle extrGain;

    public GameObject updatePanel;
    public Text age;
    public Text gender;
    public Text height;
    public Text weight;
    public Text af;
    public Text goal;

    public GameObject newHeightWeightPanel;
    public GameObject newHeightInput;
    public GameObject newWeightInput;

    public GameObject newActivityLevelPanel;
    public Toggle newLv1;
    public Toggle newLv2;
    public Toggle newLv3;
    public Toggle newLv4;
    public Toggle newLv5;

    public GameObject newWeightGoalPanel;
    public GameObject changeGoalWarning;
    public GameObject restartPanel2;
    public Toggle newExtrLoss;
    public Toggle newNormLoss;
    public Toggle newMildLoss;
    public Toggle newMaintain;
    public Toggle newMildGain;
    public Toggle newNormGain;
    public Toggle newExtrGain;

    public GameObject petPanel;
    public Toggle isRabbit;
    public Toggle isHamster;
    public Toggle isPuppy;
    public Toggle isCat;

    public GameObject searchMealPanel;
    public Dropdown categorys;
    public Dropdown meals;
    public Text mealText;
    public Text calorieText;

    public GameObject newMealPanel;
    public Dropdown newCategorys;
    public GameObject newMealInput;
    public GameObject newServingInput;
    public GameObject newCalorieInput;

    public GameObject nameText;

    public GameObject happyIcon;
    public GameObject sadIcon;

    public HealthBar happinessBar;
    public GameObject happinessText;
    
    public GameObject intakeText;
    public HealthBar calorieBar;
    public GameObject goalText;
    public GameObject text;
    
    public GameObject targetBar;
    public GameObject targetBound;
    public Text targetText;
    public GameObject idealBar;
    public GameObject idealBound;
    public Text idealText;

    public GameObject background;
    public Sprite[] backgroundOptions;

    public GameObject disappearPanel;
    public GameObject shrivelPanel;
    public GameObject mutationPanel;
    public GameObject explodePanel;

    public GameObject noInputPanel;
    public Text error;

    public GameObject goodJobPanel;

    public void getAuth() {
        Debug.Log(AuthHandler.idToken);
        Debug.Log(AuthHandler.userId);
    }

    public void getDT() {
        Debug.Log(DateTime.Now);
    }

    public void testStarving() {
        PlayerPrefs.SetInt("form",-1);
        instantiatePet(PlayerPrefs.GetInt("form"), PlayerPrefs.GetInt("type"), PlayerPrefs.GetInt("outfit"));
    }

    public void testDissapear() {
        PlayerPrefs.SetInt("form",-2);
        instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
        pet.GetComponent<Animator>().SetBool("Disappear",true);
        toggleOff(home);
        toggleOn(disappearPanel);
    }

    public void signUp() {
        SoundManagerScript.PlaySound("click");
        string fullName = fullNameInput.GetComponent<InputField>().text;
        string email = emailInput.GetComponent<InputField>().text;
        string password = passwordInput.GetComponent<InputField>().text;
        string passwordConfirm = passwordConfirmInput.GetComponent<InputField>().text;
        if (fullName == "" ) {
            error.text = "name not filled in";
            toggleOn(noInputPanel);
        } else if (email == "") {
            error.text = "email not filled in";
            toggleOn(noInputPanel);
        } else if (!email.Contains("@gmail.com")) {
            error.text = "email is incorrect, requires gmail";
            toggleOn(noInputPanel);
        } else if (password == "") {
            error.text = "password not filled in";
            toggleOn(noInputPanel);
        } else if (password != passwordConfirm) {
            error.text = "passwords does not match";
            toggleOn(noInputPanel);
        } else if (password.Length < 8) {
            error.text = "password cannot be less than 8 letters";
            toggleOn(noInputPanel);
        } else {
            AuthHandler.SignUp(email, password, new User(fullName,email,password));
            toggleOn(verifyEmailPanel);
        }
    }

    public void login() {
        SoundManagerScript.PlaySound("click");
        string email = loginEmailInput.GetComponent<InputField>().text;
        string password = loginPasswordInput.GetComponent<InputField>().text;
        AuthHandler.SignIn(email, password);
        if (remember.isOn) {
            PlayerPrefs.SetString("email",email);
            PlayerPrefs.SetString("password",password);
        }
        toggleOff(loginPanel);
        toggleOn(emptyPanel);
        StartCoroutine("wait");
    }

    public void hidePassword() {
        string password = loginPasswordInput.GetComponent<InputField>().text;
    }

    IEnumerator wait() {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(3);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        toggleOff(emptyPanel);
        toggleOn(loadingPanel);
    }

    public void autoLogin() {
        AuthHandler.SignIn(PlayerPrefs.GetString("email"), PlayerPrefs.GetString("password"));
    }

    private void Start() {
        if (!PlayerPrefs.HasKey("tutorial")) {
            toggleOn(tutorial);
            PlayerPrefs.SetString("tutorial","done");
        }
        if (!PlayerPrefs.HasKey("then")) {
            PlayerPrefs.SetString("then",DateTime.Now.ToString());
        }
        if (!PlayerPrefs.HasKey("email") || !PlayerPrefs.HasKey("password")) {
            toggleOn(startPanel);
        } else {
            toggleOn(loginPanel);
            loginEmailInput.GetComponent<InputField>().text = PlayerPrefs.GetString("email");
            loginPasswordInput.GetComponent<InputField>().text = PlayerPrefs.GetString("password");
            /*
            autoLogin();
            if (!PlayerPrefs.HasKey("login") || PlayerPrefs.GetString("login") == "fail") {
                toggleOn(loginPanel);
            } else {
                if (!PlayerPrefs.HasKey("name") || !PlayerPrefs.HasKey("age") || !PlayerPrefs.HasKey("gender") || !PlayerPrefs.HasKey("height") || !PlayerPrefs.HasKey("weight") || !PlayerPrefs.HasKey("af") || !PlayerPrefs.HasKey("goal") || !PlayerPrefs.HasKey("text") || !PlayerPrefs.HasKey("target") || !PlayerPrefs.HasKey("type")) {
                    toggleOn(userDetailsPanel);
                }
            }
            */
        }
        if (!PlayerPrefs.HasKey("user")) {
            userText.GetComponent<Text>().text = "";
        }
        if (!PlayerPrefs.HasKey("target")) {
            PlayerPrefs.SetInt("target",0);
        }
        if (!PlayerPrefs.HasKey("intake")) {
            PlayerPrefs.SetInt("intake",0);
        }
        if (!PlayerPrefs.HasKey("form")) {
            PlayerPrefs.SetInt("form",0);
        }
        if (!PlayerPrefs.HasKey("outfit")) {
            PlayerPrefs.SetInt("outfit",0);
        }
        if (!PlayerPrefs.HasKey("fatDT")) {
            PlayerPrefs.SetString("fatDT","");
        }
        if (!PlayerPrefs.HasKey("thinDT")) {
            PlayerPrefs.SetString("thinDT","");
        }
        if (!PlayerPrefs.HasKey("background")) {
            PlayerPrefs.SetInt("background",0);
        }

        instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
        background.GetComponent<SpriteRenderer>().sprite = backgroundOptions[PlayerPrefs.GetInt("background")];

        DateTime now = DateTime.Now;
        DateTime then = Convert.ToDateTime(PlayerPrefs.GetString("then"));
        if(now.Year > then.Year || now.Month > then.Month || now.Day > then.Day) {
            updateStatus();
        }
        if (now.Year > then.Year) {
            int age = PlayerPrefs.GetInt("age");
            PlayerPrefs.SetInt("age",age+1);
        }

        categorySelected(categorys);
        categorys.onValueChanged.AddListener(delegate { categorySelected(categorys); });
        
        newCategorySelected(newCategorys);
        newCategorys.onValueChanged.AddListener(delegate { newCategorySelected(newCategorys); });

        InvokeRepeating("updateTime",0f,30f);
        InvokeRepeating("updateLoad",0f,0.5f);
        InvokeRepeating("updateHappinessBar",0f,0.5f);
        InvokeRepeating("updateCalorieBar",0f,0.5f);
        InvokeRepeating("updateDetails",0f,1f);
    }

    private void categorySelected(Dropdown d) {
        int index = d.value;
        string category = d.options[index].text;
        List<Food> items = new List<Food>();
        meals.options.Clear();
        DatabaseHandler.GetMeals(category, ms => {
            foreach (var m in ms) {
                items.Add(new Food(m.Value.name,m.Value.serving,m.Value.calorie));
            }
            foreach (var item in items) {
                meals.options.Add(new Dropdown.OptionData() { text = $"{ item.name } * {item.serving} * {item.calorie}" });
            }
            mealSelected(meals);
            meals.onValueChanged.AddListener(delegate { mealSelected(meals); });
        });
        if (items.Count == 0) {
            mealText.text = "";
            calorieText.text = "";
        }
    }

    private void mealSelected(Dropdown d) {
        int index = d.value;
        string meal = d.options[index].text;
        mealText.text = meal;
        int count = 0;
        string calorie = "";
        foreach (char i in meal) {
            if (count == 2 && !i.Equals(' ')) {
                calorie += i;
            } else if (i.Equals('*')) {
                count += 1;
            }
        }
        Debug.Log(count);
        Debug.Log(calorie);
        //calorieText.text = meal.Substring(meal.Length - 3);
        calorieText.text = calorie;
    }

    private void newCategorySelected(Dropdown d) {
        int index = d.value;
        string category = d.options[index].text;
        PlayerPrefs.SetString("category", category);
    }

    public void mealOld() {
        SoundManagerScript.PlaySound("click");
        int calorie = int.Parse(0 + calorieText.text);
        int intake = PlayerPrefs.GetInt("intake") + calorie;
        PlayerPrefs.SetInt("intake", intake);
        eat();
        toggleOff(searchMealPanel);
        toggleOnHome();
    }

    public void mealNew() {
        string category = PlayerPrefs.GetString("category");
        string name = newMealInput.GetComponent<InputField>().text;
        string serving = newServingInput.GetComponent<InputField>().text;
        int calorie = int.Parse(0 + newCalorieInput.GetComponent<InputField>().text);
        if (name == "" || serving == "" || calorie == 0) {
            SoundManagerScript.PlaySound("click");
            toggleOn(noInputPanel);
        } else {
            SoundManagerScript.PlaySound("click");
            Food meal = new Food(name,serving,calorie);
            DatabaseHandler.PostMeal(category,name,serving,meal, () => { },AuthHandler.idToken);
            int intake = PlayerPrefs.GetInt("intake") + calorie;
            PlayerPrefs.SetInt("intake", intake);
            eat();
            toggleOff(newMealPanel);
            toggleOnHome();
        }
    }

    public void eat() {
        int goal = PlayerPrefs.GetInt("goal");
        int form = PlayerPrefs.GetInt("form");
        int target = PlayerPrefs.GetInt("target");
        int intake = PlayerPrefs.GetInt("intake");
        if (goal < 0) {
            if (form == 0 && intake > target) {
                PlayerPrefs.SetInt("form",1);
                PlayerPrefs.SetString("fatDT",DateTime.Now.ToString());
                changeHappiness(-50);
                instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                toggleOn(mutationPanel);
            } else if (form == 1 && intake > target) {
                PlayerPrefs.SetInt("form",2);
                instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                pet.GetComponent<Animator>().SetBool("Explode",true);
                toggleOn(explodePanel);
            } else {
                toggleOnHome();
                changeHappiness(10);
                petJump();
            }
        } else if (goal > 0 ) {
            toggleOnHome();
            changeHappiness(10);
            petJump();
        } else {
            if (form == 0 && intake > target) {
                PlayerPrefs.SetInt("form",1);
                PlayerPrefs.SetString("fatDT",DateTime.Now.ToString());
                changeHappiness(-50);
                instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                toggleOn(mutationPanel);
            } else if (intake > target) {
                PlayerPrefs.SetInt("form",2);
                instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                pet.GetComponent<Animator>().SetBool("Explode",true);
                toggleOn(explodePanel);
            } else {
                toggleOnHome();
                changeHappiness(10);
                petJump();
            }
        }
    }

    private void updateStatus() {
        int goal = PlayerPrefs.GetInt("goal");
        int form = PlayerPrefs.GetInt("form");
        int target = PlayerPrefs.GetInt("target");
        int intake = PlayerPrefs.GetInt("intake");
        if (goal < 0) {
            if (form == 1) {
                TimeSpan fatTS = DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("fatDT"));
                if (fatTS.TotalDays >= 7) {
                    PlayerPrefs.SetInt("form",0);
                    PlayerPrefs.SetString("fatDT","");
                    changeHappiness(25);
                    instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                    toggleOff(home);
                    toggleOn(goodJobPanel);
                }
            }
        } else if (goal > 0) {
            if (form == 0) {
                if (intake < target) {
                    PlayerPrefs.SetInt("form",-1);
                    PlayerPrefs.SetString("thinDT",DateTime.Now.ToString());
                    changeHappiness(-50);
                    instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                    toggleOff(home);
                    toggleOn(shrivelPanel);
                }
            } else if (form == -1) {
                TimeSpan thinTS = DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("thinDT"));
                if (intake < target) {
                    PlayerPrefs.SetInt("form",-2);
                    instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                    pet.GetComponent<Animator>().SetBool("Disappear",true);
                    toggleOff(home);
                    toggleOn(disappearPanel);
                } else if (thinTS.TotalDays >= 7) {
                    PlayerPrefs.SetInt("form",0);
                    PlayerPrefs.SetString("thinDT","");
                    changeHappiness(25);
                    instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                    toggleOff(home);
                    toggleOn(goodJobPanel);
                }
            }
        } else {
            if (form == -1) {
                TimeSpan thinTS = DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("thinDT"));
                if (intake < (target-500)) {
                    PlayerPrefs.SetInt("form",-2);
                    instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                    pet.GetComponent<Animator>().SetBool("Disappear",true);
                    toggleOff(home);
                    toggleOn(disappearPanel);
                } else if (thinTS.TotalDays >= 7) {
                    PlayerPrefs.SetInt("form",0);
                    PlayerPrefs.SetString("thinDT","");
                    changeHappiness(25);
                    instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                    toggleOff(home);
                    toggleOn(goodJobPanel);
                }
            } else if (form == 0) {
                if(intake < (target-500)) {
                    PlayerPrefs.SetInt("form",-1);
                    PlayerPrefs.SetString("thinDT",DateTime.Now.ToString());
                    changeHappiness(-50);
                    instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                    toggleOff(home);
                    toggleOn(shrivelPanel);
                }
            } else if (form == 1) {
                TimeSpan fatTS = DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("fatDT"));
                if (intake < (target-500)) {
                    PlayerPrefs.SetInt("form",-2);
                    instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                    pet.GetComponent<Animator>().SetBool("Disappear",true);
                    toggleOff(home);
                    toggleOn(disappearPanel);
                } else if (fatTS.TotalDays >= 7) {
                    PlayerPrefs.SetInt("form",0);
                    PlayerPrefs.SetString("fatDT","");
                    changeHappiness(25);
                    instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
                    toggleOff(home);
                    toggleOn(goodJobPanel);
                }
            }
        }
        PlayerPrefs.SetInt("intake", 0);
    }

    private void updateTime() {
        PlayerPrefs.SetString("then",DateTime.Now.ToString());
    }

    private void updateLoad() {
        if (PlayerPrefs.GetString("login") == "success") {
            loadingText.text = "successfully logged in as \n " + PlayerPrefs.GetString("user");
            toggleOff(againButton);
            toggleOn(beginButton);
        } else {
            loadingText.text = "log in failed";
            toggleOff(beginButton);
            toggleOn(againButton);
        }
    }

    public void begin() {
        SoundManagerScript.PlaySound("click");
        toggleOff(loadingPanel);
        if (!PlayerPrefs.HasKey("name") || !PlayerPrefs.HasKey("age") || !PlayerPrefs.HasKey("gender") || !PlayerPrefs.HasKey("height") || !PlayerPrefs.HasKey("weight") || !PlayerPrefs.HasKey("af") || !PlayerPrefs.HasKey("goal") || !PlayerPrefs.HasKey("text") || !PlayerPrefs.HasKey("target") || !PlayerPrefs.HasKey("type")) {
            toggleOn(userDetailsPanel);
        }
    }

    public void again() {
        SoundManagerScript.PlaySound("click");
        toggleOff(loadingPanel);
        toggleOn(loginPanel);
    }

    private void updateHappinessBar() {
        float happiness = (float) pet.GetComponent<Pet>().happiness;
        float size = happiness/100f;
        happinessBar.SetSize(size);
        if (size < .3f) {
            happinessBar.SetColor(Color.red);
            toggleOff(happyIcon);
            toggleOn(sadIcon);
        } else {
            happinessBar.SetColor(Color.white);
            toggleOff(sadIcon);
            toggleOn(happyIcon);
        }
    }

    private void updateCalorieBar() {
        float calorie = (float) PlayerPrefs.GetInt("intake");
        float target = (float) PlayerPrefs.GetInt("target");
        float size = calorie/target;
        if (size >= 1) {
            calorieBar.SetSize(1f);
        } else if (size > 0) {
            calorieBar.SetSize(size);
        } else {
            calorieBar.SetSize(0f);
        }
        if (size > .7f) {
            calorieBar.SetColor(Color.red);
        } else {
            calorieBar.SetColor(Color.white);
        }
        if (PlayerPrefs.GetInt("goal") == 0) {
            toggleOn(targetBar);
            toggleOn(idealBar);
            Vector3 targetPos = targetBound.GetComponent<RectTransform>().position;
            Vector3 idealPos = idealBound.GetComponent<RectTransform>().position;
            float targetLimit = 1.393229f;
            float idealLimit = 1.393229f;
            if (target != 0) {
                float limit = 2f*1.393229f;
                targetLimit = 1.393229f-limit*(1f-((target-500f)/target));
                idealLimit = 1.393229f-limit*(1f-((target-250f)/target));
            }
            targetBound.GetComponent<RectTransform>().position = new Vector3(targetLimit, targetPos.y, targetPos.z);
            idealBound.GetComponent<RectTransform>().position = new Vector3(idealLimit, idealPos.y, idealPos.z);
            targetText.GetComponent<Text>().text = (target-500f).ToString();
            idealText.GetComponent<Text>().text = (target-250f).ToString();
        } else {
            toggleOff(targetBar);
            toggleOff(idealBar);
        }
        text.GetComponent<Text>().text = PlayerPrefs.GetString("text");
    }

    private void Update() {
        nameText.GetComponent<Text>().text = PlayerPrefs.GetString("name");
        happinessText.GetComponent<Text>().text = "" + pet.GetComponent<Pet>().happiness;
        goalText.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("target");
        intakeText.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("intake");
        userText.GetComponent<Text>().text = PlayerPrefs.GetString("user");
    }

    private void updateDetails() {
        age.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("age");
        gender.GetComponent<Text>().text = PlayerPrefs.GetString("gender");
        height.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("height") + " cm";
        weight.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("weight") + " kg";

        int a = PlayerPrefs.GetInt("af");
        string activity = "";
        if (a == 1) {
            activity = "sedentary";
        } else if (a == 2) {
            activity = "light";
        } else if (a == 3) {
            activity = "moderate";
        } else if (a == 4) {
            activity = "heavy";
        } else if (a == 5) {
            activity = "very heavy";
        }
        af.GetComponent<Text>().text = activity;

        int g = PlayerPrefs.GetInt("goal");
        string goalT = "";
        if (g == -3) {
            goalT = "extreme weight loss";
        } else if (g == -2) {
            goalT = "normal weight loss";
        } else if (g == -1) {
            goalT = "mild weight loss";
        } else if (g == 0) {
            goalT = "maintain weight";
        } else if (g == 1) {
            goalT = "mild weight gain";
        } else if (g == 2) {
            goalT = "normal weight gain";
        } else if (g == 3) {
            goalT = "extreme weight gain";
        }
        goal.GetComponent<Text>().text = goalT;
    }

    public void saveUserDetails() {
        SoundManagerScript.PlaySound("click");
        string name = nameInput.GetComponent<InputField>().text;
        int age = int.Parse(0 + ageInput.GetComponent<InputField>().text);
        if (name == "") {
            error.text = "pet's name not filled in";
            toggleOn(noInputPanel);
        } else if (name.Length > 10) {
            error.text = "pet's name too long";
            toggleOn(noInputPanel);
        } else if (age < 5 || age > 100) {
            error.text = "inaccurate information";
            toggleOn(noInputPanel);
        } else {
            PlayerPrefs.SetString("name",name);
            PlayerPrefs.SetInt("age",age);
            if (isM.isOn) {
                PlayerPrefs.SetString("gender","M");
            } else if (isF.isOn) {
                PlayerPrefs.SetString("gender","F");
            }
            toggleOff(userDetailsPanel);
            toggleOn(heightWeightPanel);
        }
    }

    public void saveHeightWeight() {
        SoundManagerScript.PlaySound("click");
        int height = int.Parse(0 + heightInput.GetComponent<InputField>().text);
        int weight = int.Parse(0 + weightInput.GetComponent<InputField>().text);
        if (height < 50 || height > 300 || weight < 10 || weight > 450) {
            error.text = "inaccurate information";
            toggleOn(noInputPanel);
        } else {
            PlayerPrefs.SetInt("height", height);
            PlayerPrefs.SetInt("weight", weight);
            calculateIntake();
            toggleOff(heightWeightPanel);
            toggleOn(activityLevelPanel);
        }
    }

    public void saveActivityLevel() {
        SoundManagerScript.PlaySound("click");
        int af = 0;
        if (Lv1.isOn) {
            af = 1;
        } else if (Lv2.isOn) {
            af = 2;
        } else if (Lv3.isOn) {
            af = 3;
        } else if (Lv4.isOn) {
            af = 4;
        } else if (Lv5.isOn) {
            af = 5;
        }
        PlayerPrefs.SetInt("af",af);
        toggleOff(activityLevelPanel);
        toggleOn(weightGoalPanel);
    }

    public void saveWeightGoal() {
        SoundManagerScript.PlaySound("click");
        int goal = 0;
        if (extrLoss.isOn) {
            goal = -3;
        } else if (normLoss.isOn) {
            goal = -2;
        } else if (mildLoss.isOn) {
            goal = -1;
        } else if (maintain.isOn) {
            ;
        } else if (mildGain.isOn) {
            goal = 1;
        } else if (normGain.isOn) {
            goal = 2; 
        } else if (extrGain.isOn) {
            goal = 3;
        }
        PlayerPrefs.SetInt("goal", goal);
        calculateIntake();

        int target = PlayerPrefs.GetInt("target");
        if (goal == 0) {
            if ((target-500) < 1200) {
                toggleOn(restartPanel1);
            } else {
                toggleOff(weightGoalPanel);
                toggleOn(petPanel);
            }
        } else {
            if (target < 1200) {
                toggleOn(restartPanel1);
            } else {
                toggleOff(weightGoalPanel);
                toggleOn(petPanel);
            }
        }
    }

    public void restart_1() {
        SoundManagerScript.PlaySound("click");
        toggleOff(restartPanel1);
        toggleOff(weightGoalPanel);
        toggleOn(userDetailsPanel);
    }

    public void set1200_1() {
        SoundManagerScript.PlaySound("click");
        int goal = PlayerPrefs.GetInt("goal");
        if (goal == 0) {
            PlayerPrefs.SetInt("target", 1200+500);
            toggleOff(restartPanel1);
            toggleOff(weightGoalPanel);
            toggleOn(petPanel);
        } else {
            PlayerPrefs.SetInt("target", 1200);
            toggleOff(restartPanel1);
            toggleOff(weightGoalPanel);
            toggleOn(petPanel);
        }
    }
    
    public void updateHeightWeight() {
        SoundManagerScript.PlaySound("click");
        int height = int.Parse(0 + newHeightInput.GetComponent<InputField>().text);
        int weight = int.Parse(0 + newWeightInput.GetComponent<InputField>().text);
        if (height < 50 || height > 280 || weight < 10 || weight > 450) {
            error.text = "inaccurate information";
            toggleOn(noInputPanel);
        } else {
            PlayerPrefs.SetInt("height", height);
            PlayerPrefs.SetInt("weight", weight);
            calculateIntake();
            toggleOff(newHeightWeightPanel);
        }
    }

    public void updateActivityLevel() {
        SoundManagerScript.PlaySound("click");
        int af = 0;
        if (newLv1.isOn) {
            af = 1;
        } else if (newLv2.isOn) {
            af = 2;
        } else if (newLv3.isOn) {
            af = 3;
        } else if (newLv4.isOn) {
            af = 4;
        } else if (newLv5.isOn) {
            af = 5;
        }
        PlayerPrefs.SetInt("af",af);
        calculateIntake();
        toggleOff(newActivityLevelPanel);
    }

    public void updateWeightGoal() {
        SoundManagerScript.PlaySound("click");

        PlayerPrefs.SetInt("previousGoal",PlayerPrefs.GetInt("goal"));
        PlayerPrefs.SetInt("previousTarget",PlayerPrefs.GetInt("target"));

        int goal = 0;
        if (newExtrLoss.isOn) {
            goal = -3; 
        } else if (newNormLoss.isOn) {
            goal = -2;  
        } else if (newMildLoss.isOn) {
            goal = -1;
        } else if (newMaintain.isOn) {
            ;
        } else if (newMildGain.isOn) {
            goal = 1; 
        } else if (newNormGain.isOn) {
            goal = 2;
        } else if (newExtrGain.isOn) {
            goal = 3;
        }
        
        PlayerPrefs.SetInt("goal", goal);
        calculateIntake();

        int target = PlayerPrefs.GetInt("target");
        if (goal == 0) {
            if ((target-500) < 1200) {
                toggleOn(restartPanel2);
            } else {
                changeIntake(0);
                toggleOff(changeGoalWarning);
                toggleOff(newWeightGoalPanel);
                toggleOnHome();
            }
        } else {
            if (target < 1200) {
                toggleOn(restartPanel2);
            } else {
                changeIntake(0);
                toggleOff(changeGoalWarning);
                toggleOff(newWeightGoalPanel);
                toggleOnHome();
            }
        }
    }
    public void cancel_2() {
        SoundManagerScript.PlaySound("click");
        PlayerPrefs.SetInt("goal",PlayerPrefs.GetInt("previousGoal"));
        PlayerPrefs.SetInt("target",PlayerPrefs.GetInt("previousTarget"));
        PlayerPrefs.DeleteKey("previousGoal");
        PlayerPrefs.DeleteKey("previousTarget");
        calculateIntake();
        toggleOff(restartPanel2);
        toggleOff(changeGoalWarning);
        toggleOff(newWeightGoalPanel);
        toggleOnHome();
    }

    public void restart_2() {
        SoundManagerScript.PlaySound("click");
        toggleOff(restartPanel2);
        toggleOff(changeGoalWarning);
        toggleOff(newWeightGoalPanel);
        toggleOn(userDetailsPanel);
    }

    public void set1200_2() {
        SoundManagerScript.PlaySound("click");
        int goal = PlayerPrefs.GetInt("goal");
        if (goal == 0) {
            PlayerPrefs.SetInt("target", 1200+500);
            toggleOff(restartPanel2);
            toggleOff(changeGoalWarning);
            toggleOff(newWeightGoalPanel);
            toggleOnHome();
        } else {
            PlayerPrefs.SetInt("target", 1200);
            toggleOff(restartPanel2);
            toggleOff(changeGoalWarning);
            toggleOff(newWeightGoalPanel);
            toggleOnHome();
        }
    }

    public void calculateIntake() {
        double target = (6.25*PlayerPrefs.GetInt("height")) + (10*PlayerPrefs.GetInt("weight")) - (5*PlayerPrefs.GetInt("age"));
        string gender = PlayerPrefs.GetString("gender");
        if (gender == "M") {
            target += 5;
        } else if (gender == "F") {
            target -= 161;
        }
        int af = PlayerPrefs.GetInt("af");
        if (af == 1) {
            target *= 1.2;
        } else if (af == 2) {
            target *= 1.375;
        } else if (af == 3) {
            target *= 1.55;
        } else if (af == 4) {
            target *= 1.725;
        } else if (af == 5) {
            target *= 1.9;
        }
        int goal = PlayerPrefs.GetInt("goal");
        string text = "";
        if (goal == -3) {
            target -= 1000;
            text = "limit";
        } else if (goal == -2) {
            target -= 500;
            text = "limit";
        } else if (goal == -1) {
            target -= 250;
            text = "limit";
        } else if (goal == 0) {
            target += 250;
            text = "limit";
        } else if (goal == 1) {
            target += 250;
            text = "target";
        } else if (goal == 2) {
            target += 500;
            text = "target";
        } else if (goal == 3) {
            target += 1000;
            text = "target";
        }
        PlayerPrefs.SetString("text",text);
        PlayerPrefs.SetInt("target", (int) target);
    }
    
    public void createPet() {
        SoundManagerScript.PlaySound("click");
        if (isRabbit.isOn) {
            PlayerPrefs.SetInt("type",0);
        } else if (isHamster.isOn) {
            PlayerPrefs.SetInt("type",1);
        } else if (isPuppy.isOn) {
            PlayerPrefs.SetInt("type",2);
        } else if (isCat.isOn) {
            PlayerPrefs.SetInt("type",3);
        }
        instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),PlayerPrefs.GetInt("outfit"));
        toggleOff(petPanel);
    }

    public void changeBackground(int i) {
        changeHappiness(5);
        background.GetComponent<SpriteRenderer>().sprite = backgroundOptions[i];
        PlayerPrefs.SetInt("background",i);
    }

    public void changeOutfit(int i) {
        changeHappiness(5);
        instantiatePet(PlayerPrefs.GetInt("form"),PlayerPrefs.GetInt("type"),i);
        PlayerPrefs.SetInt("outfit",i);
    }

    public void restart() {
        pet.GetComponent<Pet>().restartPet();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Menu");
    }

    public void restart2() {
        SoundManagerScript.PlaySound("click");
        changeHappiness(100);
        PlayerPrefs.SetInt("intake",0);
        PlayerPrefs.SetInt("form",0);
        PlayerPrefs.SetInt("outfit",0);
        PlayerPrefs.SetString("fatDT","");
        PlayerPrefs.SetString("thinDT","");
        PlayerPrefs.SetInt("background",0);
        toggleOff(disappearPanel);
        toggleOnHome();
        toggleOn(weightGoalPanel);
    }

    public void restart3() {
        SoundManagerScript.PlaySound("click");
        SceneManager.LoadScene("Menu");
    }

    public void quit() {
        pet.GetComponent<Pet>().savePet();
        Application.Quit();
    }

    public void toggleOff(GameObject g) {
        if (g.activeInHierarchy) {
            g.SetActive(false);
        }
    }

    public void toggleOn(GameObject g) {
        if (!g.activeInHierarchy) {
            g.SetActive(true);
        }
    }

    public void toggleOnHome() {
        toggleOn(home);
        if (PlayerPrefs.GetInt("goal") == 0) {
            toggleOn(bars);
        }
    }

    public void toggleOutfitPanel() {
        int form = PlayerPrefs.GetInt("form");
        int type = PlayerPrefs.GetInt("type");
        if (form == -1) {
            toggleOn(thinPanels[type]);
        } else if (form == 0) {
            toggleOn(normPanels[type]);
        } else if (form == 1) {
            toggleOn(fatPanels[type]);
        } else {
            toggleOn(blankPanel);
        }
    }

    public void changeHappiness(int i) {
        pet.GetComponent<Pet>().updateHappiness(i);
        pet.GetComponent<Pet>().saveHappiness();
    }

    public void changeIntake(int i) {
        PlayerPrefs.SetInt("intake",i);
    }

    public void changeTarget(int i) {
        PlayerPrefs.SetInt("target",i);
    }

    public void petJump() {
        if (pet.transform.position.y < -1.5f) {
            pet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,10000));
            SoundManagerScript.PlaySound("jump");
        }
    }

    private void instantiatePet(int form, int type, int outfit) {
        if (pet) {
            Destroy(pet);
        }
        if (form == -1) {
            if (type == 0) {
                pet = Instantiate(rabbitThinList[outfit], new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
            } else if (type == 1) {
                pet = Instantiate(hamsterThinList[outfit], new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
            } else if (type == 2) {
                pet = Instantiate(puppyThinList[outfit], new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
            } else {
                pet = Instantiate(catThinList[outfit], new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
            }
        } else if (form == 0) {
            if (type == 0) {
                pet = Instantiate(rabbitNormList[outfit], new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
            } else if (type == 1) {
                pet = Instantiate(hamsterNormList[outfit], new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
            } else if (type == 2) {
                pet = Instantiate(puppyNormList[outfit], new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
            } else {
                pet = Instantiate(catNormList[outfit], new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
            }
        } else if (form == 1) {
            if (type == 0) {
                pet = Instantiate(rabbitFatList[outfit], new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
            } else if (type == 1) {
                pet = Instantiate(hamsterFatList[outfit], new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
            } else if (type == 2) {
                pet = Instantiate(puppyFatList[outfit], new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
            } else {
                pet = Instantiate(catFatList[outfit], new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
            }
        } else {
            pet = Instantiate(emptyPet, new Vector3(0, -2f, 0), Quaternion.identity) as GameObject;
        }
    }
}