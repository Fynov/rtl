using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

public class LoadOnClick : MonoBehaviour {

    [SerializeField]
    private Text registersuccess = null;
    [SerializeField]
    private Text usernametext = null;
    [SerializeField]
    private Text passtext = null;
    [SerializeField]
    private Text mailtext = null;
    [SerializeField]
    private Text loginsuccess = null;
    [SerializeField]
    private Text usernamelogin = null;
    [SerializeField]
    private Text passlogin = null;
    int getlvl = 1;
    int error = 2;

    public void loadScene(int level){
		Application.LoadLevel(level);
	}

	public void quitThis(){
		Application.Quit();
	}

    public bool Validate(string emailaddress)
    {
        string email = emailaddress;
        Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                + "@"
                                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
        Match match = regex.Match(email);
        if (match.Success)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void login()
    {
        if (usernamelogin.text != "" && passlogin.text != "")
        {
            WWWForm empty = new WWWForm();
            string url = "http://test7293.comli.com/check_scores.php";
            string hash = "securitystuff";
            string formNick = usernamelogin.text;
            string formPassword = passlogin.text;
            int formLvl = 1; //nov level, pol pa pogledam če je u bazi večji

            WWWForm form = new WWWForm();
            form.AddField("myform_hash", hash); //hash and hash :D preveri če je isti uporabnik (sql)
            form.AddField("myform_nick", formNick);//check nick
            form.AddField("myform_pass", formPassword);//check pass
            //form.AddField("myform_lvl", formLvl);//level check
            WWW www = new WWW(url, form);


            loginsuccess.text = "Logging in...\n Please Wait!";
            StartCoroutine(WaitForRequest(www));
            form = empty;
            //if (error == 0)
            //{
            //    loginsuccess.text = "Success!";
            //    Application.LoadLevel(getlvl);
            //}
        }
        else
        {
            loginsuccess.text = "Fill in everything.";
        }
    }


    
    public void register()
    {
        if (usernametext.text != "" && passtext.text != "" && mailtext.text != "")
        {
            WWWForm empty = new WWWForm();
            string url = "http://test7293.comli.com/check_scores1.php";
            string hash = "securitystuff";
            string formNick = usernametext.text;
            string formPassword = passtext.text;
            string formMail = mailtext.text;
            int formLvl = 1;


            if (Validate(formMail))
            {
                WWWForm form = new WWWForm();
                form.AddField("myform_nick", formNick);//check nick
                form.AddField("myform_pass", formPassword);//check pass
                form.AddField("myform_mail", formMail);//mail fill in
                form.AddField("myform_lvl", formLvl);//level check
                WWW www = new WWW(url, form);

                registersuccess.text = "Registering...\n Please wait.";
                StartCoroutine(WaitForRegister(www));
                form = empty;
            }
            else
            {
                registersuccess.text = "Invalid Email!\n Try again.";
            }
        }
        else
        {
            registersuccess.text = "Fill in everything.";
        }
    }

    public void back1()
    {
        Application.LoadLevel(0);
    }

    public void back2()
    {
        Application.LoadLevel("Login");
    }

    public void gotoLogin()
    {
        Application.LoadLevel("Login Form");
    }

    public void gotoRegister()
    {
        Application.LoadLevel("Register Form");
    }

    public void gotoLoginRegister()
    {
        Application.LoadLevel("Login");
    }


    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // errorji?
        if (!string.IsNullOrEmpty(www.error))
        {
            loginsuccess.text = "Error, try again!";
            error = 1;
        }
        else
        {
            //return level number
            //levele je treba po vrsti dat, pa ostale spremenit v build options (dat druge številke)
            string glvl = www.text;
            glvl = glvl.Substring(0,glvl.IndexOf("<"));
            int.TryParse(glvl, out getlvl);
            if (getlvl == 1 || getlvl == 2 || getlvl == 3 || getlvl == 4 || getlvl == 5 || getlvl == 6)
            {
                error = 0;
                Application.LoadLevel(getlvl);
            }
            else
            {
                error = 1;
                loginsuccess.text = glvl;
            }
        }
    }

    IEnumerator WaitForRegister(WWW www)
    {
        yield return www;

        // errorji?
        if (!string.IsNullOrEmpty(www.error))
        {
            loginsuccess.text = "Error, try again!";
            error = 1;
        }
        else
        {
            //return
            string glvl = www.text;
            glvl = glvl.Substring(0, glvl.IndexOf("<"));
            registersuccess.text = glvl;
        }
    }
    
    //IN GAME OPTIONS (Gumbi)
    public void destroyMe()
    {
        Destroy(GameObject.Find("DestroyMe"));
        Time.timeScale = 1;
    }
    public void gotoMainMenu()
    {
        Application.LoadLevel(0);
    }
}
