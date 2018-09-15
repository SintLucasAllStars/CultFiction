<?php
    $con = mysqli_connect('localhost','root', 'root','unityaccess');

    //check that connection happened
    if(mysqli_connect_errno()) {
        echo "1 Connection failed"; // error code #1 = connection failed
        exit();
    }

    $username = $_POST["username"];
    $newscore = $_POST["score"];
    $newmoney = $_POST["Money"];
    $headbandunlocked = $_POST["UnlockedHeadband"];
    $glassesunlocked = $_POST["UnlockedGlasses"];
    $jewelryunlocked = $_POST["UnlockedJewelry"];
    $shoeunlocked = $_POST["UnlockedShoes"];
    $headbandvalue = $_POST["HeadbandValue"];
    $glassesvalue = $_POST["GlassesValue"];
    $jewelryvalue = $_POST["JewelryValue"];
    $shoevalue = $_POST["ShoeValue"];



    //double check there is only one user with this name
    $namecheckquery = "SELECT username FROM players WHERE username= '".$username . "';";

    $namecheck = mysqli_query($con, $namecheckquery) or die("2 Name check query failed"); // error code #2 - name check query failed
    if(mysqli_num_rows($namecheck) != 1){
        echo "5: Either no user with name, or more than one";//error code #5 number of names matching doesn't != 1
        exit();
    }

    $updatequery =
        "UPDATE players 
        SET score = " . $newscore . ",
        Money = " . $newmoney . ",
        UnlockedHeadband = " . $headbandunlocked . ",
        UnlockedGlasses = " . $glassesunlocked . ",
        UnlockedShoes = " . $shoeunlocked . ",
        UnlockedJewelry = " . $jewelryunlocked . ",
        HeadbandValue = " . $headbandvalue . ",
        GlassesValue = " . $glassesvalue . ",
        JewelryValue = " . $jewelryvalue . ",
        ShoeValue = " . $shoevalue . "
        
         
         
         WHERE username = '" . $username . "';";
    mysqli_query($con, $updatequery) or die ("7: Save query failed");// error code #7 - update query failed

    echo"0";

?>