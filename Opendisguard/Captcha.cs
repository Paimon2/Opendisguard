using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    internal class Captcha
    {
        public enum LetterRestrictions
        {
            IncludeCapitals = 0,
            IncludeNumbers = 1,
            IncludeLowercase = 2,
        }

        /*
         * @desc Returns 1 single letter based on the flags passed
         * to it (see LetterRestrictions)
         * 
         * @param restrictions The restrictions passed as flags
         */
        public static String randomLetter(LetterRestrictions restrictions)
        {
            // Initialise our RNG and string builder
            var random = new Random();

            var builder = new StringBuilder();

            // Sets of allowed characters per each flag
            var capitals = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var lowercase = "abcdefghijklmnopqrstuvwxyz";
            var numbers = "0123456789";

            // Check restrictions for each flag and append allowed characters
            // to our StringBuilder object

            if (restrictions.HasFlag(LetterRestrictions.IncludeCapitals))
                for (int i = 0; i < capitals.Length; i++)
                    builder.Append(capitals[i]);

            if (restrictions.HasFlag(LetterRestrictions.IncludeLowercase))
                for (int i = 0; i < lowercase.Length; i++)
                    builder.Append(lowercase[i]);

            if (restrictions.HasFlag(LetterRestrictions.IncludeNumbers))
                for (int i = 0; i < numbers.Length; i++)
                    builder.Append(numbers[i]);


            // Get the string from our builder
            String completeString = builder.ToString();

            // Just in case the user's stupid and didn't pass us any flags
            if (completeString.Length == 0)
                return "";

            // Return a random letter by selecting a random index
            // from the new String of allowed characters
            return completeString[random.Next(completeString.Length)].ToString();
        }

        /*
         * @desc Returns a matrix of a captcha image with all the fiddly-bits figured out.
         * @return Matrix of a captcha image.
         */
        public static Mat getCaptchaImage()
        {
            Random rnd = new Random();

            // Make a new 200x450 canvas
            Mat img1 = new Mat(200, 450, MatType.CV_8U, 3);

            // Draw a white overlay rectangle (as it is originally black)
            Cv2.Rectangle(img1,
                          new Point(0, 0), new Point(600, 200),
                          rnd.Next(100, 255),
                          -1);

            // Our captcha text will be here
            String captchaText = "";

            /* Loop 6 times over, selecting a random character each time,
             * appending it to our captchaText object.
             * 
             * We also draw this character to our base canvas from earlier.
             */
            for (int i = 0; i < 6; i++)
            {
                String selectedLetter = randomLetter(
                    LetterRestrictions.IncludeCapitals |
                    LetterRestrictions.IncludeNumbers |
                    LetterRestrictions.IncludeLowercase);

                if (selectedLetter == "")
                    continue;

                captchaText += selectedLetter;

                Array values = Enum.GetValues(typeof(HersheyFonts));
                // Pick a random font for this character
                HersheyFonts randomFont = (HersheyFonts)values.GetValue(rnd.Next(values.Length));

                // Add the character!
                Cv2.PutText(img1,
                    selectedLetter,
                    new Point(rnd.Next(60, 80) * i, rnd.Next(50, 150)),
                    randomFont,
                    3,
                    new Scalar(0, 0, 0));
            }

            // Blur the entire image ever so slightly before returning it as a Mat
            Cv2.GaussianBlur(img1, img1, new Size(23, 23), 1);


            return img1;
        }
    }

