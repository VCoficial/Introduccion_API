<?php 
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "BD_API";
$nombre=$_GET['nom'];
$celular=$_GET['cel'];
$id=$_GET['id'];

try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  $stmt = $conn->prepare("UPDATE persona SET nombre='".$nombre."', celular=".$celular." WHERE id=".$id);
  $stmt->execute();
  echo "Editado";

  //editarConsulta.php?nom=goku&cel=8
  
} catch(PDOException $e) {
  echo "Error: " . $e->getMessage();
}
$conn = null;
echo "</table>";
?>