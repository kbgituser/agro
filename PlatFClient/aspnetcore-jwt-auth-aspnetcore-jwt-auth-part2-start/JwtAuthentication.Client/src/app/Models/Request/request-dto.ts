import { BaseEntity } from "../classes/base-entity";

export class RequestDto extends BaseEntity {
    id: number;
    price :number;
    title: string;
    userId: number;
    cityId: number; 
    message:string;
    startDate: Date;
    endDate: Date;    
    requestStatus: number;
    categoryId: number;
        
}
