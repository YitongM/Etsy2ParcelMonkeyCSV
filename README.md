# What is Etsy2ParcelMonkeyCSV?

Etsy2ParcelMonkeyCSV is a simple program designed for small business who sells on Etsy and wants to use Parcel Monkey's bulk shipping for all your orders. It reads the Etsy exported orders CSV file, and convert all the informations to a Parcel Monkey bulk shipping CSV uploader compatible file. Because the Etsy file is not compatible with Parcel Monkey and we didn't find any usable tool online. And to copy/paste sender and recipient information one by one manually is very tedious. So I wrote this program in my free time to help out a friend and to save people time! Please enjoy :)

## How to use Etsy2ParcelMonkeyCSV?
1. Download the Etsy2ParcelMonkeyCSV_v1.0.zip, and unzip it.
2. Edit the config file: Etsy2ParcelMonkeyCSV.exe.config, fill all the informations between ```<appSettings>``` tags. (Only for the first time, no need to edit it each time)
3. Export the orders from Etsy, and put the exported CSV file somewhere on your drive and open that folder.
4. Run Etsy2ParcelMonkeyCSV.exe
5. Copy/paste the folder's path in the opened console window
6. Copy/paste the file's complete name including the .csv extention
7. It will export a .csv file in the same folder of the Etsy2ParcelMonkeyCSV program, inside the /Exports folder
8. Go to https://www.parcelmonkey.com/ and log in your account, upload this file using their bulk shipping tool :)

I find open source important. And I enjoy contributing back to the community.
If this tool helped you as well, please feel free to [buy me a coffee](https://paypal.me/opaleLBC) :blush: Thank you!