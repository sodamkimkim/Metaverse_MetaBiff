<?php

$servername = "localhost:3306";
$username = "root";
$password = "asd123";
$dbname = "biff";

$nickname = $_POST["nickname"];

$conn = new mysqli($servername, $username, $password, $dbname);
if($conn->connect_error)
{
	die("connection failed : ". $conn->connect_error);
}
// select * from characterInfo where  userId = "theka265";
// select * from characterInfo where NickName = "sodam1";
$sql = "delete from characterInfo where NickName = '".$nickname."'";
$conn->query($sql);

echo $sql;
$conn->close();
?>