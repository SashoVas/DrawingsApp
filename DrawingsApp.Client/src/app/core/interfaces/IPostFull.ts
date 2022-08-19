import { IComment } from "./IComment";

export interface IPostFull{
    id:string,
    imgUrls:Array<string>,
    description:string,
    postedOn:string,
    title:string,
    likes:number,
    user:{
        userId:string,
        userName:string
    },
    group:{
        groupId:number,
        groupName:string,
        groupType:number
    }
    role:number,
    comments:Array<IComment>
}