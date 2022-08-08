export interface IComment{
    id:string,
    contents:string,
    userId:string,
    userName:string,
    commentedOn:string,
    comments:Array<IComment>
}