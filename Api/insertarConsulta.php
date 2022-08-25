<?php 
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "BD_API";
$nombre=$_GET['nom'];
$celular=$_GET['cel'];

try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  $stmt = $conn->prepare("INSERT INTO persona(nombre, celular) VALUES ('".$nombre."',".$celular.")");
  $stmt->execute();
  echo "Agregado";
  
  //insertarConsulta.php?nom=goku&cel=8
  
} catch(PDOException $e) {
  echo "Error: " . $e->getMessage();
}
$conn = null;
echo "</table>";
?>