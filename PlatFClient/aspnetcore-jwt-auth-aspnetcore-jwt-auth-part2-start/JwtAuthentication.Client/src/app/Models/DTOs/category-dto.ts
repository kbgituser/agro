import { BaseEntity } from "../classes/base-entity";

export class CategoryDto extends BaseEntity {
    // id: number;
    // name: string;
    // code: number;
    parentCategoryId: number | null;
    childrenCategories: number[];
}
