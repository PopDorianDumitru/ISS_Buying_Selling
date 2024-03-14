using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2Validator
{

    public class ValidatorException: Exception
    {
        private List<string> errors;
        public ValidatorException(List<string> _errors) 
        {
            this.errors = _errors;
        }

        public List<string> getErrors()
        {
            return errors;
        }
    }
    public class Validator
    {

        //Checks if email has the following properties: longer than 0, contains @, contains .com or .gmail or .hotmail
        public void validateEmail(string email)
        {
            List<string> errors = new List<string>();

            if(email.Length == 0)
            {
                errors.Add("Email is compulsory");
            }

            if (!email.Contains("@"))
            {
                errors.Add("Email address does not contain @");
            }
            if (!email.Contains(".com") && !email.Contains(".gmail") && !email.Contains(".hotmail"))
            {
                errors.Add("Email address does not contain domain");
            }
            if(errors.Count > 0)
            {
                throw new ValidatorException(errors);
            }
        }

        //Checks that password is at least 8 characters long, contains at least one capital letter, contains at least one
        //special character
        public void validatePassword(string password)
        {
            List<string> errors = new List<string>();
            if (password.Length < 8)
            {
                errors.Add("Password must be at least 8 characters long");
            }
            int i;
            for (i = 0; i < password.Length; i++)
            {
                if (password[i] <= 'Z' && password[i] >= 'A')
                {
                    break;
                }
            }

            if (i == password.Length)
            {
                errors.Add("Password must contain capital letters");
            }

            char[] charSet = { '~', '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '{', '}', '[', ']', '|', ':', ';', '"', '\'', '<', ',', '>', '.', '?', '/', '\\' };

            bool containsSpecialCharacter = password.Any(c =>  charSet.Contains(c));

            if (!containsSpecialCharacter)
            {
                errors.Add("Password must contain at least one special character");
            }

            if(errors.Count == 0)
            {
                throw new ValidatorException(errors);
            }
        }
        //Checks wether date is of valid format dd/mm/yyyy, and it's validity taking into account leap years,
        //also checks wether user is at least 18 years old
        public void validateDate(string date)
        {
            List<string> errors = new List<string>();
            if(date.Length == 0)
            {
                errors.Add("Date is compulsory");
            }

            for(int i = 0; i < date.Length; i++)
            {
                if (date[i] != '/' && !char.IsDigit(date[i]))
                {
                    errors.Add("Date can only contain letters and /");
                    break;
                }
            }

            // Validating after format: dd/mm/yyyy
            string[] elems = date.Split('/');
            if(elems.Length != 3)
            {
                errors.Add("Incorrect date format");
            }

            int day = int.Parse(elems[0]);
            int month = int.Parse(elems[1]);
            int year = int.Parse(elems[2]);
            //Console.WriteLine(day);
            //Console.WriteLine(month);
            //Console.WriteLine(year);
            if(month > 12)
            {
                errors.Add("Incorrect Date");
            } else if(year < 1900 || year > 2100)
            {
                errors.Add("Incorrect date");
            } else if(month == 8 && day > 31)
            {
                errors.Add("Incorrect date");
            } else if(month != 2 && month % 2 == 0 && day > 30)
            {
                errors.Add("Incorrect date");
            } else if(month != 2 && month % 2 == 1 && day > 31)
            {
                errors.Add("Incorrect date");
            } else if(month == 2 && year % 4 == 0 && day > 29)
            {
                errors.Add("Inorrect date");
            } else if(month == 2 && day > 28)
            {
                errors.Add("Incorrect date");
            } else if(int.Parse(DateTime.Now.Year.ToString()) - year < 18)
            {
                errors.Add("User must at least 18 years old");
            } else if(int.Parse(DateTime.Now.Year.ToString()) - year == 18)
            {
                
                if(int.Parse(DateTime.Now.Month.ToString()) < month)
                {
                    errors.Add("User must be at least 18 years old");
                } else if(int.Parse(DateTime.Now.Month.ToString()) == month)
                {
                    if(int.Parse(DateTime.Now.Day.ToString()) < day)
                    {
                        errors.Add("User must be at least 18 years old");
                    }
                }
            }

            if(errors.Count > 0)
            {
                throw new ValidatorException(errors);
            }
            
        }

        //Checks wether phone number starts with 07, contains only digits and has exactly 10 characters
        public void validatePhoneNumber(string number)
        {
            List<string> errors = new List<string>();

            foreach(char c in number)
            {
                if (!char.IsDigit(c))
                {
                    errors.Add("Phone number should only consist of digits");
                    break;
                }
            }

            if (number.Substring(0, 2) != "07")
            {
                errors.Add("Phone number should start with 07");
            }
            if(number.Length != 10)
            {
                errors.Add("Phone number should have exactly 10 digits");
            }

            if(errors.Count > 0)
            {
                throw new ValidatorException(errors);
            }
        }


        //Checks wether the price is valid, meaning it can contain at most one ., and is only made of digits
        public void validatePrice(string price)
        {
            List<string> errors = new List<string>();
            bool hasDot = false;
            foreach(char c in price)
            {
                if(c == '.' && hasDot == false)
                {
                    hasDot = true;
                } else if(hasDot == true)
                {
                    errors.Add("Incorrect format");
                    break;
                }
                if (!char.IsDigit(c) && c != '.')
                {
                    if (c == '-')
                    {
                        errors.Add("Price has to be a positive number");
                        break;
                    }
                    else
                    {
                        errors.Add("Price has to be a number an cannot contain special characters or letters");
                    }
                }
            }
            if(errors.Count > 0)
            {
                throw new ValidatorException(errors);
            }
        }

    }
}
