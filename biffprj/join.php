<?php

$servername = "localhost:3306";
$username = "root";
$password = "asd123";
$dbname = "biff";

$id = $_POST["id"];
$pw = $_POST["pw"];

$conn = new mysqli($servername, $username, $password, $dbname);
if($conn->connect_error)
{
	die("connection failed : ". $conn->connect_error);
}
// Insert into userInfo(userId, userPassword) values("theka265", "asd123");
$sql = "Insert into userInfo(userId, userPassword) values('".$id."', '".$pw."')";
// $result = $conn->query($sql);
$conn->query($sql);
echo $sql;

$conn->close();
?>