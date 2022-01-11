# 注意几个坑

- 如果需要反序列化的是字符串
  - 一定要写上初始值，如下
  - public string Name { get; set; } = string.Empty;

- 如果需要反射的类有父类(不管是不是虚类)，
  - 需要反射的属性
  - 读写权限一定要高于private,就是说最低是protected
  -  public float x { get; protected set; }