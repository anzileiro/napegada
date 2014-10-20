function inserirRacas(){
	var racas = db.getSiblingDB('napegada').getCollection('raca');
	
	var valores = [
		{
			Nome: 'Angorá',
			Especie: 0
		},
		{
			Nome: 'Siamês',
			Especie: 0
		},
		{
			Nome: 'Persa',
			Especie: 0
		},
		
		
		
		{
			Nome: 'Labrador',
			Especie: 1
		},
		{
			Nome: 'Golden',
			Especie: 1
		},
		{
			Nome: 'Pastor Alemão',
			Especie: 1
		}
	];
	
	racas.insert(valores);
}