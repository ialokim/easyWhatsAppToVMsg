# easyWhatsAppToVMsg
C# WhatsApp-TXT-Export ot vMessage converter

I've just switched from Windows Phone to Android and I didn't want to loose all my WhatsApp messages (there is no native method to transfer them to Android, see https://www.whatsapp.com/faq/en/wp/28060005).

I've used the "email chat history" feature (see https://www.whatsapp.com/faq/en/wp/22548236) to get the messages outside WhatsApp. Now, I could have imported them in the Android WhatsApp database directly, but in order to be able to read/write the database, I had to root my phone.

So, I've thought of importing the messages as being normal text messages instead of WhatsApp messages. I've found the Android app "VMG Converter" (https://play.google.com/store/apps/details?id=com.aoe.vmgconverter&hl=en, free, but 2.18 euros to import more than 200 messages a time), so I've decided to write a little program converting the exported WhatsApp messages to vMessage files readable by VMG Converter.

Tested and works without errors with TXT-files from a German Windows Phone WhatsApp export, feel free to contribute and test it with another OS or language!
