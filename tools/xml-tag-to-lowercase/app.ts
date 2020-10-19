import {
  std_fs,
  std_path,
} from './deps.ts';

import {parse, serialize, Document as XMLDocument, Xml} from './xml.ts';

const {walk} = std_fs;
const {extname} = std_path;

class App {

  files: std_fs.WalkEntry[] = [];

  tagToLowerCase = (parent?: Xml) => {
    if (!parent) {
      return;
    }

    parent.name = parent.name.toLowerCase();
    
    for (let child of parent.children) {
      this.tagToLowerCase(child);
    }
  };

  markFiles = async () => {
    for await (const entry of walk("./workshop")) {
      if (entry.isFile && extname(entry.name) === ".xml") {

        this.files.push(entry);
        
      }
    }
  };

  transform = async () => {

    for (let file of this.files) {
      let text: string = await Deno.readTextFile(file.path);
      
      const deserializedObject = parse(text);

      if (deserializedObject.root) {
        this.tagToLowerCase(deserializedObject.root);
        await Deno.writeTextFile(file.path, serialize(deserializedObject.root));
      }

    }

  };

  initialize = async () => {
    await this.markFiles();
    await this.transform();
  }

  /**
   *
   */
  constructor() {
    this.initialize();
  }


}

export default new App();