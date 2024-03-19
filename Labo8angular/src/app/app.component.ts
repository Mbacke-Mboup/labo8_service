import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Animal } from 'src/models/animal';
import {lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  // Inputs
  id ?: number;
  type : string = "";
  name : string = "";

  animals : Animal[] = [];
  animal ?: Animal;

  constructor(public http : HttpClient){}

  // Récupère tous les animaux dans la base de données
  async getAnimals() : Promise<void>{
	 let x = await lastValueFrom(this.http.get<Animal[]>("http://localhost:5024/api/Animals/GetAnimal"));
   this.animals = x;
  }

  // Ajoute un animal dans la base de données
  async postAnimal() : Promise<void>{
    let animal = new Animal(0,this.type,this.name)
     await lastValueFrom(this.http.post<Animal>("http://localhost:5024/api/Animals/PostAnimal", animal));
    this.getAnimals()


  }

  // Récupère un animal en particulier dans la base de données
  async getAnimal() : Promise<void>{
    let x = await lastValueFrom(this.http.get<Animal>("http://localhost:5024/api/Animals/GetAnimal/"+this.id));
    this.animal = x
  }

  // Modifie (ou crée) un animal en particulier dans la base de données
  async putAnimal() : Promise<void>{
    let animal = new Animal(this.id!,this.type,this.name)
     await lastValueFrom(this.http.put<Animal>("http://localhost:5024/api/Animals/PutAnimal/"+this.id, animal));
    this.getAnimals()

  }

  // Supprime un animal en particulier dans la base de données
  async deleteAnimal() : Promise<void>{
    await lastValueFrom(this.http.delete("http://localhost:5024/api/Animals/destroy/"+this.id));
    this.getAnimals()
  }

  // Sussy function
  async deleteAll() : Promise<void>{
    await lastValueFrom(this.http.delete("http://localhost:5024/api/Animals/destroy"));
    this.getAnimals()
  }

}
